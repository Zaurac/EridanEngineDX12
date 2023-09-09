using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EridanEditor.Components
{
    interface IMComponent { }    

    [DataContract]
    abstract class Component : ViewModelBase
    {
        [DataMember]
        public GameEntity Owner { get; private set; }

        public Component(GameEntity owner)
        {
            Debug.Assert(owner != null);    
            Owner = owner;
        }
    }

    abstract class MSComponent<T> : ViewModelBase, IMComponent where T : Component 
    { 

    }
}
