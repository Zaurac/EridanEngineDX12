#pragma once
#include "../Components/ComponentsCommon.h"
#include "TransformComponent.h"
#include "ScriptComponent.h"

namespace eridan {
	namespace game_entity {

		DEFINE_TYPED_ID(entity_id);
		class entity
		{
		public:
			constexpr explicit entity(entity_id id) : _id{ id } {}
			constexpr entity() : _id{ id::invalid_id } {}
			constexpr entity_id get_id() const { return _id; };
			constexpr bool is_valid() const { return id::is_valid(_id); };

			transform::component transform() const;
			script::component script() const;
		private:
			entity_id _id;
		};
	} // namespace game_entity

	namespace script
	{
		class entity_script : public game_entity::entity
		{
		public:
			virtual ~entity_script() = default;
			virtual void begin_play() {}
			virtual void update(float) {}
		protected:
			constexpr explicit entity_script(game_entity::entity entity) : game_entity::entity{ entity.get_id() } {}
		};

		namespace detail 
		{
			using script_ptr = std::unique_ptr<entity_script>;
			using script_creator = script_ptr(*)(game_entity::entity);
			u8 register_script(size_t, script_creator);
			using string_hash = std::hash<std::string>;
			template<class script_class>
			script_ptr create_script(game_entity::entity entity)
			{
				assert(entity.is_valid());
				return std::make_unique<script_class>(entity);
			};

#define REGISTER_SCRIPT(TYPE)                                           \
		class TYPE;                                                     \
        namespace {                                                     \
        const u8 _reg_##TYPE                                            \
        { eridan::script::detail::register_script(                      \
              eridan::script::detail::string_hash()(#TYPE),             \
              &eridan::script::detail::create_script<TYPE>) };          \
        }

		} // namespace detail															

	} // namespace script
}