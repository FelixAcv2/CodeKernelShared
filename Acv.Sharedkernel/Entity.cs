using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel
{

    public class Entity
    {
        int _Id;
        int? _requestedHashCode;

        public virtual int ID
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        public bool IsTransient()
        {
            return this._Id == 0;
        }

        public void ChangeCurrentIdentity(int identity)
        {
            if (identity != 0)
                this.ID = identity;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.ID == this.ID;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.ID.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }


    }

}
