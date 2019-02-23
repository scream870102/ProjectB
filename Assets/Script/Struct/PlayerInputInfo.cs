[System.Serializable]
public struct PlayerInputInfo
{
    public EColor color;
    public float time;
    public PlayerInputInfo(EColor color,float time){
        this.color=color;
        this.time=time;
    }   
}
