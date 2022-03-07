namespace Csp_TilePlacement
{
    public class State
    {
        public State()
        {
            Data=new int[4][];
        }
        public State(State state)
        {
          Array.Copy(state.Data,Data,state.Data.Length);
        }
        public int[][] Data { get; set; }
        public State Previous { get; set; }
    }
}
