$(function(){
    $(".informationBox").niceScroll({
        cursorcolor: "#6e3b1b", // �ı��������ɫ��ʹ��16������ɫֵ
        cursoropacitymin: 0, // ��������������״̬ʱ�ı�͸����, ֵ��Χ 1 �� 0
        cursoropacitymax: 1, // ������������ʾ״̬ʱ�ı�͸����, ֵ��Χ 1 �� 0
        cursorwidth: "10px", // �������Ŀ�ȣ���λ������
        cursorborder: "1px solid #e6af61", // CSS��ʽ����������߿�
        cursorborderradius: "5px", // ������Բ�ǣ����أ�
        zindex: "auto" | 999, // �ı��������DIV��z-indexֵ
        scrollspeed: 60, // �����ٶ�
        mousescrollstep: 40, // �����ֵĹ����ٶ� (����)
        touchbehavior: false, // ������ק����
        hwacceleration: true, // ����Ӳ������
        autohidemode: scroll, // ���ع������ķ�ʽ, ���õ�ֵ:
        background: "#e6af61", // ����ı�����ɫ
        iframeautoresize: true, // �ڼ����¼�ʱ�Զ�����iframe��С
        cursorminheight: 32, // ���ù���������С�߶� (����)
        preservenativescrolling: true, // ��������������ɹ�������Ĺ������������������¼�
        railoffset: false, // ����ʹ��top/left������λ��
        bouncescroll: false, // (only hw accell) ���ù�����Ծ�������ƶ�
        spacebarenabled: true, // �����¿ո�ʱʹҳ�����¹���
        railpadding: { top: 0, right: 0, left: 0, bottom: 0 }, // ���ù�����ڼ��
        disableoutline: true, // ��ѡ��һ��ʹ��nicescroll��divʱ��chrome������н���outline
        horizrailenabled: true, // nicescroll���Թ���ˮƽ����
        enabletranslate3d: true, // nicescroll ����ʹ��CSS��������������
        enablemousewheel: true, // nicescroll���Թ����������¼�
        enablekeyboard: true, // nicescroll���Թ�������¼�
        smoothscroll: true, // ease��������
        sensitiverail: true, // ���������������
        enablemouselockapi: true, // �������������API���� (���ƶ����϶�)
        cursorfixedheight: false, // �������ĸ߶ȣ����أ�
        hidecursordelay: 400, // ���ù������������ӳ�ʱ�䣨���룩
        directionlockdeadzone: 6, // �趨������Ϊ��������������أ�
        nativeparentscrolling: true, // ������ݵײ������ø�������
        enablescrollonselection: true, // ��ѡ���ı�ʱ���������Զ�����
    });

})