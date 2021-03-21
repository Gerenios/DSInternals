namespace DSInternals.Replication.Model
{
    //using DSInternals.Common;
    //using DSInternals.Common.Data;
    using System;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Text;

    public class ReplicaObject
    {
        private string distinguishedName;
        private Guid guid;
        private SecurityIdentifier sid;

        public ReplicaObject(String distinguishedName, Guid objectGuid, SecurityIdentifier objectSid, ReplicaAttributeCollection attributes)
        {
            this.guid = objectGuid;
            this.distinguishedName = distinguishedName;
            this.sid = objectSid;
            this.Attributes = attributes;
        }

        public string DistinguishedName
        {
            get
            {
                return this.distinguishedName;
            }
        }
        public Guid Guid
        {
            get
            {
                return this.guid;
            }
        }

        public SecurityIdentifier Sid
        {
            get
            {
                return this.sid;
            }
        }

        // TODO: Read only collection
        public ReplicaAttributeCollection Attributes
        {
            get;
            private set;
        }

        public void LoadLinkedValues(ReplicatedLinkedValueCollection linkedValueCollection)
        {
            var objectAttributes = linkedValueCollection.Get(this.Guid);

            // Only continue if the linked values contain attributes of this AD object
            if(objectAttributes != null)
            {
                foreach (var attribute in objectAttributes)
                {
                    this.Attributes.Add(attribute);
                }
            }
        }

    }
}
