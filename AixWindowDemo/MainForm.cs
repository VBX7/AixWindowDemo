using AixWindowDemo.Biz;
using AixWindowDemo.Model;
using System;
using System.Windows.Forms;

namespace AixWindowDemo
{
    public partial class MainForm : Form
    {
        private CaculationBiz biz;
        public MainForm()
        {
            this.biz = new CaculationBiz();
            InitializeComponent();

        }

        /// <summary>
        /// 开始计算面阵参数
        /// </summary>
        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                bool isFlyShot = cb_speed.Checked;
                Camera2DFocModel camera2DFocModel = new Camera2DFocModel();
                //计算视野,赋值
                if (rb_focal.Checked == true)
                {
                    camera2DFocModel.focType = Camera2DFocType.Focal;
                    camera2DFocModel.value = Convert.ToInt32(tb_focal.Text);
                }
                else
                {
                    camera2DFocModel.focType = Camera2DFocType.Scale;
                    camera2DFocModel.value = Convert.ToDouble(tb_magni.Text);
                }

                Camera2DSpeedModel camera2DSpeedModel = null;
                if (isFlyShot)//飞拍选中
                {
                    camera2DSpeedModel = new Camera2DSpeedModel();
                    camera2DSpeedModel.pixelNumber = Convert.ToDouble(tb_pixel.Text);
                    //计算飞拍
                    if (rb_speed.Checked == true)
                    {
                        camera2DSpeedModel.speedType = Camera2DSpeedType.Speed;
                        camera2DSpeedModel.value = Convert.ToDouble(tb_speed.Text);
                    }
                    else
                    {
                        camera2DSpeedModel.speedType = Camera2DSpeedType.Exposure;
                        camera2DSpeedModel.value = Convert.ToDouble(tb_exposure.Text);
                    }
                }
                Camera2DModel mode = new Camera2DModel
                {
                    distance = Convert.ToDouble(tb_WD.Text),//工作距离
                    width = Convert.ToDouble(tb_pixelWeight.Text),//分辨率宽 
                    height = Convert.ToDouble(tb_pixelHeight.Text),//分辨率高
                    pixelSize = Convert.ToDouble(tb_pixelSize.Text),//像元
                    camera2DFocModel = camera2DFocModel,
                    camera2DSpeedModel = camera2DSpeedModel
                };
                Camera2DOutput output = null;
                Camera2DSpeedModel speedOutput;
                this.biz.Camra2DFlyshotCompute(mode, isFlyShot, out output, out speedOutput);

                tb_sensorHeight.Text = output.sensorSizeHeight.ToString("#0.00");  //保留小数点后两位有效数字
                tb_sensorWeight.Text = output.sensorSizeWidth.ToString("#0.00");  //保留小数点后两位有效数字

                //计算视野
                tb_FovHeight.Text = output.fovHeight.ToString("#0.00");
                tb_FovWeight.Text = output.fovWidth.ToString("#0.00");

                //计算单像素精度
                tb_pixelAccuracy.Text = output.pixelAccuracy.ToString("#0.00");

                //计算镜头解析度
                tb_resolution.Text = output.lensResolution.ToString("#0.00");

                //计算等效倍率
                tb_magVal.Text = output.magnification.ToString("#0.000");

                if (isFlyShot)//飞拍选中
                {
                    //计算飞拍
                    if (rb_speed.Checked == true)
                    {
                        tb_exposure.Text = speedOutput.value.ToString();
                    }
                    else
                    {
                        tb_speed.Text = speedOutput.value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("部分值为空  或  " + ex.Message);
            }
        }

        private void rb_focal_Click(object sender, EventArgs e)
        {
            tb_magni.ReadOnly = true;
            tb_focal.ReadOnly = false;
        }

        private void rb_magni_Click(object sender, EventArgs e)
        {
            tb_focal.ReadOnly = true;
            tb_magni.ReadOnly = false;
        }

        private void rb_exposure_Click(object sender, EventArgs e)
        {
            tb_speed.ReadOnly = true;
            tb_exposure.ReadOnly = false;
        }

        private void rb_speed_Click(object sender, EventArgs e)
        {
            tb_speed.ReadOnly = false;
            tb_exposure.ReadOnly = true;
        }

        /// <summary>
        /// 开始计算景深
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            CameraFieldDepthOutput depthOutput = new CameraFieldDepthOutput();
            CameraFieldDepthInput depthIndex = new CameraFieldDepthInput
            {
                circleDiameter = Convert.ToDouble(tb_jsCircle.Text),
                focal = Convert.ToInt32(tb_jsFocal.Text),
                distance = Convert.ToDouble(tb_jsWD.Text),
                fno = Convert.ToDouble(tb_jsFno.Text),
            };

            depthOutput = biz.CameraFieldDepthCompute(depthIndex);

            tb_depth1.Text = depthOutput.frontFieldDepth.ToString("#0.00");
            tb_depth2.Text = depthOutput.behindFieldDepth.ToString("#0.00");
            tb_depth.Text = depthOutput.fieldDepth.ToString("#0.00");

        }

        /// <summary>
        /// 开始计算线扫参数
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //初始化镜头倍率或焦距计算
                Camera2DFocModel cameraFocModel = new Camera2DFocModel();
                if (rb2_focal.Checked == true)
                {
                    cameraFocModel.focType = Camera2DFocType.Focal;
                    cameraFocModel.value = Convert.ToDouble(tb2_focal.Text);
                }
                else
                {
                    cameraFocModel.focType = Camera2DFocType.Scale;
                    cameraFocModel.value = Convert.ToDouble(tb2_magni.Text);
                }

                //初始化相机输入参数
                CameraLineModel lineIndex = new CameraLineModel
                {
                    height = Convert.ToDouble(tb2_pixelHeight.Text),
                    width = Convert.ToDouble(tb2_pixelWeight.Text),
                    pixelSize = Convert.ToDouble(tb2_pixelSize.Text),
                    speed = Convert.ToDouble(tb2_speed.Text),
                    camera2DFocModel = cameraFocModel,
                    distance = Convert.ToDouble(tb2_WD.Text)
                };

                CameraLineOutput lineOutput = new CameraLineOutput();

                lineOutput = biz.CameraLineOutput(lineIndex);

                tb2_sensorHeight.Text = lineOutput.sensorSizeHeight.ToString("#0.00");  //靶面高度
                tb2_sensorWeight.Text = lineOutput.sensorSizeWidth.ToString("#0.00");  //靶面宽度
                tb2_fovHeight.Text = lineOutput.fovHeight.ToString("#0.00");  //视野高度
                tb2_fovWeight.Text = lineOutput.fovWidth.ToString("#0.00");  //视野宽度
                tb2_magVal.Text = lineOutput.magnification.ToString("#0.000");  //等效倍率
                tb2_pixelAccuracy.Text = lineOutput.pixelAccuracy.ToString("#0.00");  //单像素精度
                tb2_resolution.Text = lineOutput.lensResolution.ToString("#0.00");  //镜头解析力
                tb2_lineFrequency.Text = lineOutput.lineFrequency.ToString("#0.00");  //行频
            }
            catch (Exception ex)
            {
                MessageBox.Show("部分值为空  或" + ex);
            }
        }

        private void rb2_focal_Click(object sender, EventArgs e)
        {
            tb2_focal.ReadOnly = false;
            tb2_magni.ReadOnly = true;
        }

        private void rb2_magni_Click(object sender, EventArgs e)
        {
            tb2_focal.ReadOnly = true;
            tb2_magni.ReadOnly = false;
        }
    }
}
