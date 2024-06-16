public interface IUpdatable {
    public int updatePriority { get; }
    public void Update();

    public int CompareUpdatePriorityTo(IUpdatable updatable) {
        int difference = updatable.updatePriority - this.updatePriority;
        if(difference > 0) return 1;
        if(difference < 0) return -1;
        return 0;
    }
}