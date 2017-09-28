namespace FastNote
{
    public abstract class AttachedBehavior<T>
    {
        protected T AssociatedObject { get; set; }

        protected AttachedBehavior(T associatedObject)
        {
            AssociatedObject = associatedObject;
        }

        public virtual void OnAttached() { }
        public virtual void OnDetaching() { }
    }
}
