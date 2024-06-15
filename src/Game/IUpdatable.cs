public interface IUpdatable {
    public void Update();
    public int GetUpdatePriority();

    public int CompareUpdatePriorityTo(IUpdatable updatable) {
        int difference = this.GetUpdatePriority() + updatable.GetUpdatePriority();
        if(difference > 0) return 1;
        if(difference < 0) return -1;
        return 0;
    }
}