using AixWindowDemo.Dao;
using AixWindowDemo.Model;
using System;

namespace AixWindowDemo.Biz
{
    /* 这个是服务层 */
    public class CaculationBiz
    {
        private CaculationDao dao;
        public CaculationBiz()
        {
            this.dao = new CaculationDao();
        }

        //Camera2D 视野计算
        private Camera2DOutput Camra2DComputeOutput(Camera2DModel mode)
        {
            Camera2DOutput output = new Camera2DOutput();

            //计算靶面大小
            double[] sensor = this.dao.SensorSize(mode.height, mode.width, mode.pixelSize);
            output.sensorSizeHeight = sensor[0];  
            output.sensorSizeWidth = sensor[1]; ;  

            double[] fov = null;
            //计算视野
            switch (mode.camera2DFocModel.focType)
            {
                case Camera2DFocType.Focal:
                    fov = this.dao.Fov((int)mode.camera2DFocModel.value, mode.distance, sensor); //创建接受容器,视野
                    output.fovHeight = fov[0];
                    output.fovWidth = fov[1];
                    break;
                case Camera2DFocType.Scale:
                    fov = this.dao.Fov(mode.camera2DFocModel.value, mode.distance, sensor);//创建接受容器,视野
                    output.fovHeight = fov[0];
                    output.fovWidth = fov[1];
                    break;
            }

            //计算单像素精度
            output.pixelAccuracy = (fov[0] / sensor[0]) * mode.pixelSize;

            //计算镜头解析度
            output.lensResolution = 1 / (output.pixelAccuracy / 1000);

            //计算等效倍率
            output.magnification = sensor[0] / fov[0];

            return output;
        }

        //飞拍参数计算
        public void Camra2DFlyshotCompute(Camera2DModel mode, bool isFlyShot, out Camera2DOutput output, out Camera2DSpeedModel speedOutput)
        {
            try
            {
                output = Camra2DComputeOutput(mode);
                if (isFlyShot == false)
                {
                    speedOutput = null;
                }
                else
                {
                    speedOutput = new Camera2DSpeedModel();
                    switch (mode.camera2DSpeedModel.speedType)
                    {
                        case Camera2DSpeedType.Speed:
                            speedOutput.speedType = Camera2DSpeedType.Exposure;
                            speedOutput.value = ((mode.camera2DSpeedModel.pixelNumber * output.pixelAccuracy / 1000.0) / mode.camera2DSpeedModel.value) * 1000000;
                            break;
                        case Camera2DSpeedType.Exposure:
                            speedOutput.speedType = Camera2DSpeedType.Speed;
                            speedOutput.value = (mode.camera2DSpeedModel.pixelNumber * output.pixelAccuracy / 1000.0) / (mode.camera2DSpeedModel.value / 1000000);
                            break;
                        default:
                            speedOutput = null;
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //景深计算
        public CameraFieldDepthOutput CameraFieldDepthCompute(CameraFieldDepthInput depIndex)
        {
            CameraFieldDepthOutput depthOutput = new CameraFieldDepthOutput();
            double frontDepth, behindDepth, depth;

            dao.fieldDepth(depIndex, out frontDepth, out behindDepth, out depth);

            depthOutput.frontFieldDepth = frontDepth;
            depthOutput.behindFieldDepth = behindDepth;
            depthOutput.fieldDepth = depth;

            return depthOutput;
        }

        //线扫计算
        public CameraLineOutput CameraLineOutput(CameraLineModel lineModel)
        {
            CameraLineOutput lineOutput = new CameraLineOutput();

            double[] sensor = new double[2];
            double[] fov = new double[2];
            
            //计算靶面大小
            sensor = dao.SensorSize(lineModel.height, lineModel.width, lineModel.pixelSize);  //sensorS[0]为高度，sensor[1]为宽度
            lineOutput.sensorSizeHeight = sensor[0];
            lineOutput.sensorSizeWidth = sensor[1];

            //计算视野
            switch (lineModel.camera2DFocModel.focType)
            {
                case Camera2DFocType.Focal:
                    fov = dao.Fov((int)lineModel.camera2DFocModel.value, lineModel.distance, sensor);  //记住转整型
                    lineOutput.fovHeight = fov[0];
                    lineOutput.fovWidth = fov[1];  //栈里，出栈就没了，先赋值
                    break;
                case Camera2DFocType.Scale:
                    fov = dao.Fov(lineModel.camera2DFocModel.value, lineModel.distance, sensor);
                    lineOutput.fovHeight = fov[0];
                    lineOutput.fovWidth = fov[1];
                    break;
                default:
                    break;
            }

            //计算单像素精度
            lineOutput.pixelAccuracy = (fov[0] / sensor[0]) * lineModel.pixelSize;

            //计算镜头解析度
            lineOutput.lensResolution = 1 / (lineOutput.pixelAccuracy / 1000);

            //计算等效倍率
            lineOutput.magnification = sensor[0] / fov[0];

            //计算行频
            lineOutput.lineFrequency = lineModel.speed / (lineOutput.pixelAccuracy / 1000);

            return lineOutput;
        }
    }
}