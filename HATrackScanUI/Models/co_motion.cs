using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace samrtind
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct cam_polyxyva_t
    {
        public float dX;
        public float dY;
        public float dV;
        public float dA;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct cam_pointch_t
    {
        public float dX;
        public UInt16 channel0;//freq
        public UInt16 channel1;//WF(low 8bit) + SW(bit8) 
        public UInt16 channel2;//E
        public UInt16 channel3;//SE
        public float  channel4;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct CamTableRef
    {
        public Int16 element_num;
        public byte type;
        public byte vartype;
        public float start;
        public float end;
        public Int16 tappet;
//        public cam_polyxyva_t[] cam_polyxyva;
    }

    public partial class si
    {
        public const short CO_RTN_OK                    = 0;
        public const short CO_RTN_ERROR                 = -1;
        public const short CO_RTN_BUS_ERROR             = -2;
        public const short CO_RTN_INVALIDE_PARAM        = -3;
        public const short CO_RTN_CMD_FAILED            = -4;
        public const short CO_RTN_CMD_TIMEOUT           = -5;
        public const short CO_RTN_WRONG_OPMODE          = -6;
        public const short CO_RTN_MEMORY_FAULT          = -7;
        public const short CO_RTN_CHANGE_OPMODE_FAILED  = -8;
        public const short CO_RTN_CRD_FIFO_FULL         = -9;

        public const short CO_RTN_AXIS_IS_NOT_OPMODEPP  = -10;
        public const short CO_RTN_AXIS_IS_NOT_STOPPED   = -11;
        public const short CO_RTN_AXIS_IS_NOT_SERVOON   = -12;

        public const short CO_OPMODE_PP                 = 1;
        public const short CO_OPMODE_PV                 = 3;
        public const short CO_OPMODE_HOMING             = 6;
        public const short CO_OPMODE_CRD                = 128;
        public const short CO_OPMODE_CAM                = 255;

        public const short CO_SIGBIT_DRIVER_ALARM       = 0x0001;
        public const short CO_SIGBIT_POSITIVE_LIMIT     = 0x0002;
        public const short CO_SIGBIT_NEGATIVE_LIMIT     = 0x0004;
        public const short CO_SIGBIT_HOME_SWITCH        = 0x0008;

        public const short CO_MODE_PP_BUFFER            = 1;
        public const short CO_MODE_PP_IMMEDIATE         = 2;
        public const short CO_MODE_PP_CONTINUE          = 3;
        public const short CO_MODE_PP_BUFFER_REL        = 4;

        public const short CO_MODEBIT_CAM_POS_TRG       = (1 << 11);
        public const short CO_MODEBIT_CAM_PERIODIC      = (1 << 12);
        public const short CO_MODEBIT_CAM_SLV_ABS       = (1 << 13);

        public const short CO_MODE_CRD_CONTINUE         = (0);
        public const short CO_MODE_CRD_STOP             = (1);

        public const short CO_MODEBIT_PROBE_POSITIVE    = (1);
        public const short CO_MODEBIT_PROBE_NEGATIVE    = (2);
        public const short CO_MODEBIT_PROBE_ZERO        = (4);
        public const short CO_VFM_FWR                   = 1;
        public const short CO_VFM_FRD                   = 2;
        public const short CO_VFM_FPLUS                 = 4;

        /***************************************************system*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAxisNumber(IntPtr hMotion, ref int axisNumber);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDiPointNumber(IntPtr hMotion, ref int diPointNumber);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDoPointNumber(IntPtr hMotion, ref int doPointNumber);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAxisID(IntPtr hMotion, int slave_index, int axis_offset, ref int axisID);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDiPointID(IntPtr hMotion, int slave_index, int di_point_offset, ref int diPointID);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDoPointID(IntPtr hMotion, int slave_index, int do_point_offset, ref int doPointID);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_getVersion();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void co_motion_sync_task(IntPtr handle, IntPtr pArg);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_open_err(string path, IntPtr hMotion);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_open(string path);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_close(IntPtr hMotion);


        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setSyncPeriod(IntPtr hMotion, int microseconds);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_active(IntPtr hMotion);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setSyncMode(IntPtr hMotion, int mode);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_createSyncTask(IntPtr hMotion, co_motion_sync_task task, IntPtr pArg);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_destorySyncTask(IntPtr hTask);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getLinkStatus(IntPtr hMotion, ref int pStatus);
        /***************************************************Axis*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAxisStatus(IntPtr hMotion, int axis, ref int pStatus);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_clrAxisStatus(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        public static extern int co_motion_axisOn(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_axisOff(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAxisEncoderType(IntPtr handle, int axis, int type);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAxisEncoderType(IntPtr handle, int axis, ref int type);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAxisAlarmOn(IntPtr handle, int axis, int signalBits);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAxisAlarmOff(IntPtr handle, int axis, int signalBits);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAxisDiReverseOn(IntPtr handle, int axis, int signalBits);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAxisDiReverseOff(IntPtr handle, int axis, int signalBits);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setOpMode(IntPtr hMotion, int axis, int mode);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getOpMode(IntPtr hMotion, int axis, ref int mode);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getActualPosition(IntPtr hMotion, int axis, ref int position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getActualVelocity(IntPtr hMotion, int axis, ref int velocity);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAxisDi(IntPtr hMotion, int axis, ref int value);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAxisDo(IntPtr hMotion, int axis, int value);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_axisZeroPosition(IntPtr handle, int axis); 
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_axisSyncPosition(IntPtr handle, int axis, int position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setQuickStopDeceleration(IntPtr hMotion, int axis, int deceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getQuickStopDeceleration(IntPtr hMotion, int axis, ref int deceleration);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_vAxis_create(IntPtr hMotion, int slave);

        /*********************************************Profile Position*******************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPPTargetPosition(IntPtr hMotion, int axis, int position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPPTargetPosition(IntPtr hMotion, int axis, ref int position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPPVelocity(IntPtr hMotion, int axis, int velocity);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPPVelocity(IntPtr hMotion, int axis, ref int velocity);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPPAcceleration(IntPtr hMotion, int axis, int acceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPPAcceleration(IntPtr hMotion, int axis, ref int acceleration);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPPDeceleration(IntPtr hMotion, int axis, int profileDeceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPPDeceleration(IntPtr hMotion, int axis, ref int profileDeceleration);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPPBufferFlag(IntPtr hMotion, int axis, ref int flag);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_ppStart(IntPtr hMotion, int axis, int mode);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_ppHalt(IntPtr hMotion, int axis);


        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_ppAbsMove(IntPtr hMotion, int axis, int position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_ppRelMove(IntPtr hMotion, int axis, int position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_ppAbsMoveImm(IntPtr hMotion, int axis, int position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_ppRelMoveImm(IntPtr hMotion, int axis, int position);


        /**************************************************Homing************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setHomingAcceleration(IntPtr hMotion, int axis, int acceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getHomingAcceleration(IntPtr hMotion, int axis, ref int acceleration);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setHomingSpeedForSwitch(IntPtr hMotion, int axis, int speed);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getHomingSpeedForSwitch(IntPtr hMotion, int axis, ref int speed);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setHomingSpeedForZero(IntPtr hMotion, int axis, int speed);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getHomingSpeedForZero(IntPtr hMotion, int axis, ref int speed);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setHomingMethod(IntPtr hMotion, int axis, int method);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getHomingMethod(IntPtr hMotion, int axis, ref int method);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setHomingOffset(IntPtr hMotion, int axis, int homOffset);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getHomingOffset(IntPtr hMotion, int axis, ref int homOffset);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getHomingStatus(IntPtr hMotion, int axis, ref int pStatus);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_homingStart(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_homingHalt(IntPtr hMotion, int axis);

        /*********************************************Profile Velocity*******************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPVAcceleration(IntPtr hMotion, int axis, int acceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPVAcceleration(IntPtr hMotion, int axis, ref int acceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPVDeceleration(IntPtr hMotion, int axis, int deceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPVDeceleration(IntPtr hMotion, int axis, ref int deceleration);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setPVTargetVelocity(IntPtr hMotion, int axis, int velocity);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPVTargetVelocity(IntPtr hMotion, int axis, ref int velocity);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getPVStatus(IntPtr hMotion, int axis, ref int pStatus);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_pvStart(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_pvHalt(IntPtr hMotion, int axis);


        /**************************************************2D-PEG************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_peg_create(IntPtr hMotion, int slave);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_setThreshold(IntPtr hPEG, int slave);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_setAxisXSelect(IntPtr hPEG, byte axis_x_select);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_setAxisYSelect(IntPtr hPEG, byte axis_y_select);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_setOutputSelect(IntPtr hPEG, byte output_select);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_setBufferDepth(IntPtr hPEG, byte depth);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_addBufferData(IntPtr hPEG, int axis_x_position, int axis_y_position, ushort pulse_width);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_clrBuffer(IntPtr hPEG);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_getBufferCount(IntPtr hPEG, ref ushort pCount);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_getStatus(IntPtr hPEG, ref ushort pStatus);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_single(IntPtr hPEG, int axis_x_position, int axis_y_position, ushort pulse_width);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_start(IntPtr hPEG);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_peg_stop(IntPtr hPEG);
        /************************************************DIgital IO**********************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDiValue(IntPtr hMotion, int module_index, int point_position, ref byte value);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setDoValue(IntPtr hMotion, int module_index, int point_position, byte value);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDoValue(IntPtr hMotion, int module_index, int point_position, ref byte value);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDi(IntPtr hMotion, int point, ref byte value);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setDo(IntPtr hMotion, int point, byte value);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getDo(IntPtr hMotion, int point, ref byte pValue);

        /**************************************************Analog************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAiValue(IntPtr hMotion, int module_index, int channel, ref short value);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAoValue(IntPtr hMotion, int module_index, int channel, short value);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAoValue(IntPtr hMotion, int module_index, int channel, ref short pValue);

        /****************************************************CAM*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_camStart(IntPtr hMotion, int axis, int periodic);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_camHalt(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMMasterScale(IntPtr hMotion, int axis, ushort num, ushort den);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMSlaveScale(IntPtr hMotion, int axis, ushort num, ushort den);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMMasterID(IntPtr hMotion, int axis, int master_axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMMasterDir(IntPtr hMotion, int axis, int dir);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMSlaveDir(IntPtr hMotion, int axis, int dir);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMStartPosition(IntPtr hMotion, int axis, float position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMEndPosition(IntPtr hMotion, int axis, float position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMSlaveIncrement(IntPtr hMotion, int axis, float increment);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMMasterOffset(IntPtr hMotion, int axis, float offset);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMSlaveOffset(IntPtr hMotion, int axis, float offset);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_selCAMTable(IntPtr hMotion, int axis, byte index);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMTable(IntPtr hMotion, int axis, string name);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMTriggerInPosition(IntPtr hMotion, int axis, float position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCAMTriggerOutPosition(IntPtr hMotion, int axis, float position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setCamMasterSource(IntPtr hMotion, int axis, int source);
        //        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //        public static extern int co_motion_selCAMTable(IntPtr hMotion, int axis, string name);

        /****************************************************CAM DO*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_cam_do_create(IntPtr hCAMDo, int slave);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_start(IntPtr hCAMDo, int mode);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_halt(IntPtr hCAMDo);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setMasterScale(IntPtr hCAMDo, ushort num, ushort den);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setMasterID(IntPtr hCAMDo, int master_axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setMasterDir(IntPtr hCAMDo, int dir);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setStartPosition(IntPtr hCAMDo, float position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setEndPosition(IntPtr hCAMDo, float position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setMasterOffset(IntPtr hCAMDo, float offset);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_selCAMTable(IntPtr hCAMDo, byte index);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setTable(IntPtr hCAMDo, string name);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setTriggerInPosition(IntPtr hCAMDo, float position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setTriggerOutPosition(IntPtr hCAMDo, float position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_cam_do_setMasterSource(IntPtr hCAMDo, int source);
        /****************************************************Laser*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_laser_do_create(IntPtr hMotion, int slave);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_laser_do_setValue(IntPtr hLasDo, int ch, UInt16 value);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_laser_do_getValue(IntPtr hLasDo, int ch, UInt16 value);
        /****************************************************CRD*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_crd_create(IntPtr hMotion, int slave, int crd_index);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setDimension(IntPtr hCrd, ushort dimension);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setBufferDepth(IntPtr hCrd, ushort depth);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setBufferThreshold(IntPtr hCrd, ushort threshold);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setBufferEnable(IntPtr hCrd, int enable);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_getBufferSpace(IntPtr hCrd, ref ushort pCount);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_getStatus(IntPtr hCrd, ref ushort pStatus);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_clrError(IntPtr hCrd);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_getErrorCode(IntPtr hCrd, ref ushort pErrorCode);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentArcXYOCW(IntPtr hCrd, 
                                                                  int mode, 
                                                                  double target_x, 
                                                                  double target_y, 
                                                                  double origin_x, 
                                                                  double origin_y, 
                                                                  double feed_vel, 
                                                                  double end_vel, 
                                                                  double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentArcZXOCW(IntPtr hCrd, 
                                                                  int mode, 
                                                                  double target_z, 
                                                                  double target_x, 
                                                                  double origin_z, 
                                                                  double origin_x, 
                                                                  double feed_vel, 
                                                                  double end_vel, 
                                                                  double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentArcYZOCW(IntPtr hCrd, 
                                                                  int mode, 
                                                                  double target_y, 
                                                                  double target_z, 
                                                                  double origin_y, 
                                                                  double origin_z, 
                                                                  double feed_vel, 
                                                                  double end_vel, 
                                                                  double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentArcXYOCCW(IntPtr hCrd, 
                                                                   int mode, 
                                                                   double target_x, 
                                                                   double target_y, 
                                                                   double origin_x, 
                                                                   double origin_y, 
                                                                   double feed_vel, 
                                                                   double end_vel, 
                                                                   double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentArcZXOCCW(IntPtr hCrd, 
                                                                   int mode, 
                                                                   double target_z, 
                                                                   double target_x, 
                                                                   double origin_z, 
                                                                   double origin_x, 
                                                                   double feed_vel, 
                                                                   double end_vel, 
                                                                   double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentArcYZOCCW(IntPtr hCrd, 
                                                                   int mode, 
                                                                   double target_y, 
                                                                   double target_z, 
                                                                   double origin_y, 
                                                                   double origin_z, 
                                                                   double feed_vel, 
                                                                   double end_vel, 
                                                                   double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentLnXY(IntPtr hCrd, 
                                                              int mode, 
                                                              double target_x, 
                                                              double target_y, 
                                                              double feed_vel, 
                                                              double end_vel, 
                                                              double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentLnZX(IntPtr hCrd, 
                                                              int mode, 
                                                              double target_z, 
                                                              double target_x, 
                                                              double feed_vel, 
                                                              double end_vel, 
                                                              double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentLnYZ(IntPtr hCrd, 
                                                              int mode, 
                                                              double target_y, 
                                                              double target_z, 
                                                              double feed_vel, 
                                                              double end_vel, 
                                                              double max_acc);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentLnXYZ(IntPtr hCrd, 
                                                               int mode, 
                                                               double target_x, 
                                                               double target_y, 
                                                               double target_z, 
                                                               double feed_vel, 
                                                               double end_vel, 
                                                               double max_acc);
        /*
                [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
                public static extern int co_motion_crd_setAcceleration(IntPtr hCrd, int acceleration);

                [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
                public static extern int co_motion_crd_addSegment2DLn(IntPtr hCrd, uint mode, uint index, int position_x, int position_y, int velocity_max, int velocity_end);
        */
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_addSegmentGroup(IntPtr hCrd, int mode, double [] target_array, double feed_vel, double end_vel, double acc);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_endSegment(IntPtr hCrd);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_start(IntPtr hCrd);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_stop(IntPtr hCrd);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_syncAxisPosition(IntPtr hCrd);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisXOriginPosition(IntPtr hCrd, double position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisYOriginPosition(IntPtr hCrd, double position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisZOriginPosition(IntPtr hCrd, double position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisOriginPosition(IntPtr hCrd, int idx, double position);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisXScale(IntPtr hCrd, uint den, uint num);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisYScale(IntPtr hCrd, uint den, uint num);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisZScale(IntPtr hCrd, uint den, uint num);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_clrAxisMapping(IntPtr hCrd);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisXMapping(IntPtr hCrd, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisYMapping(IntPtr hCrd, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisZMapping(IntPtr hCrd, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setAxisMapping(IntPtr hCrd, int idx, int axis);

        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setLahEnable(IntPtr hCrd, 
                                                            double feed_vel, 
                                                            double max_vel, 
                                                            double corner_err, 
                                                            double acc, 
                                                            double max_acc, 
                                                            int depth);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_crd_setLahDisable(IntPtr hCrd);
        /***************************************************Probe************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_create(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_start(IntPtr hMotion, int axis, int mode);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_stop(IntPtr hMotion, int axis);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_getPositiveEdgeStatus(IntPtr hMotion, int axis, ref int status);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_getNegativeEdgeStatus(IntPtr hMotion, int axis, ref int status);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_getPositiveEdgePosition(IntPtr hMotion, int axis, ref int position);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_probe_getNegativeEdgePosition(IntPtr hMotion, int axis, ref int position);
        /****************************************************VFM*************************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr co_motion_vfm_create(IntPtr hMotion, int slave);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_vfm_open(IntPtr hVfm, string name, byte flag);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_vfm_close(IntPtr hVfm);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_vfm_write(IntPtr hVfm, byte[] buffer, int length);
        /*********************************************Absolute Encoder*******************************************************/
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_setAbsEncUpdateFlag(IntPtr hMotion, int axis, byte flag);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAbsEncUpdateFlag(IntPtr hMotion, int axis, ref byte flag);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAbsEncLoopCounter(IntPtr hMotion, int axis, ref int counter);
        [DllImport("co_motion.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int co_motion_getAbsEncSingleLoopPosition(IntPtr hMotion, int axis, ref int position);

    }
}
