   M S B A M L   `   `   `  ���� [  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  D @IDSCamera, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:IDSCamera	IDSCamera    IDSCamera.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  Iwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  #localclr-namespace:IDSCamera  -     � 
ResizeMode$  CanMinimize=� � Title$ 	IDSCamera��$	��700��$
��1000�����	#FF3E6FB3 � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ DW_Logo��$
��Left=�$	��141��$��814,513,0,0q�$	��Top=�$	��159��$��Icon/DeepWise.png���� -    ��Name$	 Log��$
��Left=�$	��172��$��	5,490,0,0q�$	��Top=�$	��790�� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       'J� �     �� " ��.��� # �@��VerticalScrollBarVisibility$
 Auto=���  � ��$ 0q���  �@��
IsReadOnly . ��  � ��$ Bottom=��� " J�.���  �@��Margin$ 0q���  � ��$ 16(�.@��� $��General�����	#FFE6F0FF.���� -   	 ��Name$	 richTextBoxGeneral�����	#FF43B5BF�� $��Warning�����	#FFE6F0FF.���� -   $	 richTextBoxWarning�����	#FF43AABF�� $��Error�����	#FFE6F0FF.���� -   $	 richTextBoxError�����	#FF43A0BF�� $��Debug�����	#FFE6F0FF.���� -   $	 richTextBoxDebug�����	#FF4396BF�� -   $ 
tabControl��$
��Left=�$��3,5,0,0q�$	��Top=�$	��478��$	��978�����	#FFE6F0FF.@��� $��Main Screen�����	#FFE6F0FF.��� .+�7  1System.Windows.Forms.Integration.WindowsFormsHost  $��	5,7,186,4q�
   Child.
 %   System.Windows.Forms.PictureBox  -	   Display_Windows��   BorderStyle$ FixedSingle=��� -
    ��Name$ Continuous_Acquisition��$
��Left=�$��
839,30,0,0q�$	��Top=�$��82��$��76�����	#FFE6F0FF$��0q� � ToolTip �� $��14(�連續取像.���� -   $! Continuous_Acquisition_Icon��$��Icon/Start.png���� -   $ Freeze��$
��Left=�$��839,134,0,0q�$	��Top=�$��82��$��76�����	#FFE6F0FF$��0q� �� $��14(�凍結取像畫面.���� -   $ Freeze_Icon��$��Icon/Freeze.png���� -   $ 
Save_Image��$
��Left=�$��839,238,0,0q�$	��Top=�$��82��$��76�����	#FFE6F0FF$��0q� �� $��14(�儲存影像.���� $��Icon/Save File.png���� -   $ Load_Parameter��$
��Left=�$��839,342,0,0q�$	��Top=�$��82��$��76�����	#FFE6F0FF$��0q� �� $��14(�導入相機參數.���� $��Icon/Load Parameter.png���� $��Camera Parameter Screen�����	#FFE6F0FF.��� .+��� -   $  Open_Camera_Parameter_File��$
��Left=�$��
116,26,0,0q�$	��Top=�$��68��$��62�����	#FFE6F0FF$��0q� �� $��14(�開啟相機參數檔路徑.���� $��Icon/Open File Path.png���� -    ��Name$ Camera_Parameter_File��   WatermarkTextBox1#��[  $��214,29,92,366q�$��12(��� -   $ Open_Save_Image_Path��$
��Left=�$��
116,95,0,0q�$	��Top=�$��68��$��62�����	#FFE6F0FF$��0q� �� $��14(�開啟儲存影像路徑.���� $��Icon/Open Image Path.png���� -   $ Save_Image_Path��  WatermarkTextBox2#��[ $��214,98,92,297q�$��12(��� -    ��Name$ Camera_Parameter��$��Camera Parameter:��$
��Left=�$	��247��$��
30,175,0,0q�$	��Top=�$	��307��$��16(����	#FFE6F0FF(  "HandyControl.Controls.TitleElement   
Background �	#FFE6F0FF.��� .+��� -    ��Name$ 
Gain_Label��$��Gain:��$
��Left=�$��	73,25,0,0q�$	��Top=�$��36��$��66��$��Center=�$��Center=�$��16(� J� RenderTransformOrigin$ 6.771,3.528&����	#FFE6F0FF���	#FFE6F0FF)  #HandyControl.Controls.NumericUpDown  -     Name$
 Gain��   Minimum$ 0Q�   Maximum$
 2000Q�   	Increment$ 1Q�$
��Left=�$��36��$��
150,25,0,0q�$	��Top=�$	��129��$��Center=�$��Center=�$��14(��� -   $ Gamma_Label��$��Gamma:��$
��Left=�$��	53,70,0,0q�$	��Top=�$��36��$��83��$��Center=�$��Center=�$��16(����	#FFE6F0FF���	#FFE6F0FF  -   $ Gamma��$ 1Q�$
 1000Q�$ 1Q�$
��Left=�$��36��$��
150,70,0,0q�$	��Top=�$	��128��$��Center=�$��Center=�$��14(��� -   $ 	FPS_Label��$
��FPS:��$
��Left=�$��
79,115,0,0q�$	��Top=�$��36��$��58��$��Center=�$��Center=�$��16(����	#FFE6F0FF���	#FFE6F0FF  -   $	 FPS��$ 1Q�$
 1000Q�$ 1Q�$
��Left=�$��36��$��150,115,0,0q�$	��Top=�$	��128��$��Center=�$��Center=�$��14(��� -   $ ExposureTime_Label��$��ExposureTime:��$
��Left=�$��	1,160,0,0q�$	��Top=�$��36��$	��138��$��Center=�$��Center=�$��16(����	#FFE6F0FF���	#FFE6F0FF  -   $ ExposureTime��$ 21Q�$ 10000000Q�$ 1Q�   DecimalPlaces$ 0X�$
��Left=�$��36��$��150,160,0,0q�$	��Top=�$	��128��$��Center=�$��Center=�$��14(��� -   $ Save_Config��$��768,233,52,0q�$	��Top=�$	��134��$��20(����	#FFE6F0FF$��0q� �� $��14(�儲存相機參數.���� $��Icon/Config.png���� $��
AOI Screen�����	#FFE6F0FF.��� 