﻿using EridanEditor.Components;
using EridanEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EridanEditor.GameProject
{
    [DataContract]
    class Scene : ViewModelBase
    {
        
        private string? _name;
        [DataMember]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [DataMember]
        public Project Project { get; private set; }

        public bool _isActive;
        [DataMember]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        [DataMember(Name = nameof(GameEntities))]
        private readonly ObservableCollection<GameEntity> _gameEntities = new ObservableCollection<GameEntity>();
        public ReadOnlyObservableCollection<GameEntity> GameEntities { get; private set; }

        public ICommand AddGameEntitiesCommand { get; private set; }
        public ICommand RemoveGameEntitiesCommand { get; private set; }

        private void AddGameEntities(GameEntity entity, int index = -1)
        {
            Debug.Assert(!_gameEntities.Contains(entity));
            entity.IsActive = IsActive;
            if(index == -1)
            {
                _gameEntities.Add(entity);
            }
            else
            {
                _gameEntities.Insert(index, entity);
            }
            
        }

        private void RemoveGameEntities(GameEntity entity)
        {
            Debug.Assert(_gameEntities.Contains(entity));
            entity.IsActive = false;
            _gameEntities.Remove(entity);
        }


        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_gameEntities != null)
            {
                GameEntities = new ReadOnlyObservableCollection<GameEntity>(_gameEntities);
                OnPropertyChanged(nameof(GameEntities));
            }

            foreach (var entity in _gameEntities)
            {
                entity.IsActive = IsActive;
            }

            AddGameEntitiesCommand = new RelayCommand<GameEntity>(x =>
            {
                AddGameEntities(x);
                var entityIndex = _gameEntities.Count - 1;
                Project.UndoRedo.Add(new UndoRedoAction(
                                   () => RemoveGameEntities(x),
                                   () => AddGameEntities(x, entityIndex),
                                   $"Add {x.Name} to {Name}"));
            });

            RemoveGameEntitiesCommand = new RelayCommand<GameEntity>(x =>
            {
                var entityIndex = _gameEntities.IndexOf(x);
                RemoveGameEntities(x);
                Project.UndoRedo.Add(new UndoRedoAction(
                                   () => AddGameEntities(x, entityIndex),
                                   () => RemoveGameEntities(x),
                                   $"Remove {x.Name} to {Name}"));
            });
        }


        public Scene(Project project, string name)
        {
            Debug.Assert(project != null);
            Project = project;
            Name = name;
            OnDeserialized(new StreamingContext());
        }
    }
}
