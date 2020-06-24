using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
 
namespace csDmc2410
{
    public class Dmc2410
    {
        //---------------------�忨��ʼ�����ú���DMC2480 ----------------------
        /********************************************************************************
        ** ��������: d2410_board_init
        ** ��������: ���ư��ʼ�������ó�ʼ�����ٶȵ�����
        ** �䡡  ��: ��
        ** �� �� ֵ: 0���޿��� 1-8���ɹ�(ʵ�ʿ���) 
        **           1001 + j: j�ſ���ʼ������ ��1001��ʼ��
        ** ��    ��:  
        ** �޸�����: 
        *********************************************************************************/
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt16 d2410_board_init();

        /********************************************************************************
        ** ��������: d2410_board_close
        ** ��������: �ر����п�
        ** �䡡  ��: ��
        ** �� �� ֵ: ��
        ** ��    ��: 
        *********************************************************************************/
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_board_close();

        /********************************************************************************
        ** ��������: ���ƿ���λ
        ** ��������: ��λ���п���ֻ���ڳ�ʼ�����֮����ã�
        ** �䡡  ��: ��
        ** �� �� ֵ: ��
        ** ��    ��: 
        *********************************************************************************/
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_board_rest", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_board_rest();

        //���������������
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_pulse_outmode(UInt16 axis, UInt16 outmode);

        //ר���ź����ú���        
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_ALM_PIN(UInt16 axis, UInt16 alm_logic, UInt16 alm_action);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_ALM_PIN_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_ALM_PIN_Extern(UInt16 axis, UInt16 alm_enbale, UInt16 alm_logic, UInt16 alm_all, UInt16 alm_action);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_EL_MODE", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_EL_MODE(UInt16 axis, UInt16 el_mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_HOME_pin_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_HOME_pin_logic(UInt16 axis, UInt16 org_logic, UInt16 filter);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_write_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_write_SEVON_PIN(UInt16 axis, UInt16 on_off);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_read_SEVON_PIN(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_RDY_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_read_RDY_PIN(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_Enable_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_Enable_EL_PIN(UInt16 axis, UInt16 enable);


        //ͨ������/������ƺ���
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_read_inbit(UInt16 cardno, UInt16 bitno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_read_outbit(UInt16 cardno, UInt16 bitno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_read_inport(UInt16 cardno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_read_outport(UInt16 cardno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_write_outport(UInt16 cardno, UInt32 port_value);

        //�ƶ�����
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_decel_stop(UInt16 axis, double Tdec);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_imd_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_imd_stop(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_emg_stop();

        //λ�����úͶ�ȡ����
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_get_position(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_position(UInt16 axis, Int32 current_position);	

        //״̬��⺯��
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt16 d2410_check_done(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt16 d2410_axis_io_status(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_rsts", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_get_rsts(UInt16 axis);

        //�ٶ����úͶ�ȡ����              
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_vector_profile(double Min_Vel, double Max_Vel, double Tacc, double Tdec);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_profile_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec,double Stop_Vel);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_s_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, Int32 Sacc, Int32 Sdec);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_st_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_st_profile(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double d2410_read_current_speed(UInt16 axis); 
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double d2410_read_vector_speed(UInt16 card);

        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_st_profile_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_st_profile_Extern(UInt16 axis, double Min_Vel, double Max_Vel, double Tacc, double Tdec, double Tsacc, double Tsdec, double Stop_Vel);

	    //���߱���/��λ
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_change_speed(UInt16 axis, double Curr_Vel);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_reset_target_position(UInt16 axis, Int32 dist);

        //���ᶨ���˶�
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_t_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_ex_t_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_ex_t_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_s_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_ex_s_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_ex_s_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);

        //���������˶�
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_s_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_s_vmove(UInt16 axis, UInt16 dir);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_t_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_t_vmove(UInt16 axis, UInt16 dir);

        //ֱ�߲岹
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_t_line2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_t_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_t_line3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_t_line3(UInt16[] axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_t_line4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_t_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode);

	    //Բ���岹
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_arc_move(UInt16[] axis, Int32[] target_pos, Int32[] cen_pos, UInt16 arc_dir);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_rel_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_rel_arc_move(UInt16[] axis, Int32[] rel_pos, Int32[] rel_cen, UInt16 arc_dir);

        //�����˶�
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi); 
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_handwheel_move(UInt16 axis);

        //��ԭ��
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_home_mode(UInt16 axis, UInt16 mode, UInt16 EZ_count);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_home_move(UInt16 axis, UInt16 home_mode, UInt16 vel_mode);

	    //ԭ������
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_homelatch_mode(UInt16 axis,UInt16 enable,UInt16 logic);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_homelatch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_get_homelatch_mode(UInt16 axis,ref UInt16 enable,ref UInt16 logic);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_get_homelatch_flag(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_reset_homelatch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_reset_homelatch_flag(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_homelatch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_get_homelatch_value(UInt16 axis);       
       
        //����λ�ñȽϺ���
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_compare_config_Extern(UInt16 card,UInt16 queue, UInt16 enable, UInt16 axis,  UInt16 cmp_source);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_get_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_compare_get_config_Extern(UInt16 card,UInt16 queue, ref UInt16 enable, ref UInt16 axis,  ref UInt16 cmp_source);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_clear_points_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_compare_clear_points_Extern(UInt16 card,UInt16 queue);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_add_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_compare_add_point_Extern(UInt16 card,UInt16 queue, UInt32  pos, UInt16 dir,  UInt16 action, UInt32  actpara);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_get_current_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32  d2410_compare_get_current_point_Extern(UInt16 card,UInt16 queue);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_get_points_runned_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32  d2410_compare_get_points_runned_Extern(UInt16 card,UInt16 queue);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_compare_get_points_remained_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32  d2410_compare_get_points_remained_Extern(UInt16 card,UInt16 queue);

	    //����λ�ñȽ�
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_config_CMP_PIN(UInt16 axis, UInt16 cmp_enable,Int32 cmp_pos,  UInt16 CMP_logic);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_config_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_get_config_CMP_PIN(UInt16 axis, ref UInt16 cmp_enable,ref Int32 cmp_pos,  ref UInt16 CMP_logic);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_read_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32  d2410_read_CMP_PIN(UInt16 axis);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_write_CMP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32  d2410_write_CMP_PIN(UInt16 axis,UInt16 on_off);
       
        //��������������
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 d2410_get_encoder(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_encoder(UInt16 axis, UInt32 encoder_value);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_get_counter_flag(UInt16 cardno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_reset_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_reset_counter_flag(UInt16 cardno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_reset_clear_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_reset_clear_flag(UInt16 cardno);

	    //��������
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_LTC_PIN_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_LTC_PIN_Extern(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode,double ltc_filter);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_latch_mode(UInt16 cardno, UInt16 all_enable);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_counter_config(UInt16 axis, UInt16 mode);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_get_latch_value(UInt16 axis);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_get_latch_flag(UInt16 cardno);
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_reset_latch_flag(UInt16 cardno);        
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_triger_chunnel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_triger_chunnel(UInt16 cardno, UInt16 num);

        [DllImport("Dmc2410.dll", EntryPoint = "d2410_set_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_set_speaker_logic(UInt16 cardno, UInt16 logic);

	    //EMG����
        [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_EMG_PIN(UInt16 cardno, UInt16 enable, UInt16 emg_logic);

	    //�����λ����
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_config_softlimit(UInt16 axis,UInt16 ON_OFF, UInt16 source_sel,UInt16 SL_action, Int32 N_limit,Int32 P_limit);
	    [DllImport("Dmc2410.dll", EntryPoint = "d2410_get_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 d2410_get_config_softlimit(UInt16 axis,ref UInt16 ON_OFF, ref UInt16 source_sel,ref UInt16 SL_action, ref Int32 N_limit,ref Int32 P_limit);

    }
}
