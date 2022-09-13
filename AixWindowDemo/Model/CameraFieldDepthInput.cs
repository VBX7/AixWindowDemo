namespace AixWindowDemo.Model
{
    /* 计算景深输入 */
    public class CameraFieldDepthInput
    {
        public double circleDiameter { set; get; }  //最小弥散圆
        public int focal { set; get; }  //焦距
        public double distance { set; get; }  //工作距离
        public double fno { set; get; }  //光圈值
    }
}
