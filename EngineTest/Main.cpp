#pragma comment(lib, "Engine.lib") 


#define TEST_ENTITY_COMPONENTS 1

#if TEST_ENTITY_COMPONENTS
#include "TestEntityComponents.h"
#else
#error One of the test need to be enabled
#endif



int main()
{
#if _DEBUG
	// Enable run-time memory check for debug builds.
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
#endif // _DEBUG

	engine_test test{};
	if (test.initialize())
	{
		test.run();
	}

	test.shutdown();
	return 0;
}