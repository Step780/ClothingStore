   M S B A M L   `   `   `  ����H  DClothingStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClothingStoreClothingStore  5          ClothingStore.Brands   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:ClothingStore     5      Y UOxyPlot.Wpf.Shared, Version=2.1.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0R NOxyPlot.Wpf, Version=2.1.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   61   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  Brands��5
      $
��1080��5      $
��1920��5      .��5      � 
�������.+�5   
   �� $��
Gosha Sans)�6   $��Light"�6,   $��Бренды��6?   $��Center=�6M   $��871,116,871,0q�6j   $	��Top=�6�   $��40(�6�   ���	#FF343A406�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
brandsGrid��5      $
��Left=�6'   $	��608��6B   $��
86,408,0,0q�6O   $	��Top=�6c   $
��1752��6{   $��0q�6�      MaterialDesignDataGrid#���   6�   ���#DEE2E6   AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Name+  5      $ Страна��6%    65   �� *	Country+4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      ��  ��Name$ Delete��6%   -   5        $��Удалить��5   5   ���#CED4DA���#CED4DA
���   �6�   5      5      5      �� 5   
   -   5         ��Name$ nameBox��5      $��0q�6#   $��50��67   $��86,342,1534,0q�6C   	 �� TextWrapping$
	 Wrap=�6Z   $��Название��6n   $	��300��6~   $��Dubai)�6�   ���	#FFCED4DA
 �� TextAlignment$

 Left=�6�   $	��Top=�6�   $��Center=�6�   $��20(�6�   .��6  ��5       �� 5"       �� 
TargetType" ��6   .�5$      ��  �� Property � �� �� Value 5%      �� 5&       �� 
TargetType" ��6.   .R�5'   "   ��  �� CornerRadius$ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   5*   #   5+      5/      51      .��52      ��54      �� 55      " ��6"   .R�56      � .+�57      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�58      �� $��Center=�6/   $��Center=�6L   5:      �� 5;       ��Name$ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5<      5>      .��5?       5@      �� 5A   
   -   5        $ 
countryBox��5A      $��0q�6&   $��50��6:   $��406,342,1214,0q�6F   $
	 Wrap=�6^   $��Страна��6r   $	��300��6�   $��Dubai)�6�   ���	#FFCED4DA$

 Left=�6�   $	��Top=�6�   $��Center=�6�   $��20(�6   .��6  ��5B      �� 5D      " ��6   .�5F      ��  � �� 5G      �� 5H      " ��6.   .R�5I   "   �� $ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   5L   #   5M      5Q      5S      .��5T      ��5V      �� 5W      " ��6"   .R�5X      � .+�5Y      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�5Z      �� $��Center=�6/   $��Center=�6L   5\      �� 5]      $ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5^      5`      .��5a       5b      �� 5c   
   -   5        $ addBtn��5c      $��Добавить��67   $
��Left=�6J   $��726,353,0,0q�6e   $	��Top=�6z   $��90��6�   $��
Gosha Sans)�6�   $��Light"�6�   $��10(�6�   ���	#FF343A40 J� Effect 5d      3  -System.Windows.Media.Effects.DropShadowEffect  5e         
BlurRadius$ 7Q�6#      Color$ 	#FFC5C5C5��62      ShadowDepth$ 3Q�6D      	Direction$	 270Q�6T      Opacity$	 0.8Q�6d      RenderingBias$ Quality=�6r   ��5g      �� 5h      " ��6   .�5i      ��  �@�Cursor$
 Hand��6/   �� 5j       � �� �#ADB5BD63    �� Triggers 5k      P� 5l       P� Property �@J�IsMouseOver P� Value . . �5m      ��  � �� �#dee2e66;   5n      5q      ��5t      �� 5u      " ��6"   .R�5v      �� $��90��6   $��35��6(   $ 10��64   #��z��6F   ._�5w      �� $��Center=�6+   $��Center=�6H   5x      5y      5{      �� 5|   
   -   5         ��Name$ comboFilter��5|      $
��Left=�6(   $��1688,343,0,0q�6C   $	��Top=�6Y   $	��150��6q   $��45��6}   $��0q�6�      MaterialDesignFilledComboBox#���  6�   ���	#FFCED4DA)  #MaterialDesignThemes.Wpf.HintAssist   Hint$' !Фильтрация страны��6  �� 5}   
   $
��Left=�6   $	��100��60   $��-166,759,0,0q�6=   $	��Top=�6S   $	��100��6k   �� 5~   
   -   5         ��Name$ 
warningBox��5~      $��Center=�6(   $��
0,-650,0,0q�6E   $
��Wrap=�6Y   $�� ��6m   
���  ��$��20(�6�    �� TextAlignment$ Center=�6�   $��Center=�6�   �� 5   
   -   5        $ btnReset��5      $��Восстановить��6#   $
��Left=�6:   $��1748,308,0,0q�6U   $	��Top=�6k   $��90��6�   $��
Gosha Sans)�6�   $��Light"�6�   $��10(�6�   ���	#FF343A40 5�        5�      $ 7Q�6#   $ 	#FFC5C5C5��62   $ 3Q�6D   $	 270Q�6T   $	 0.8Q�6d   $ Quality=�6r   ��5�      �� 5�      " ��6   .�5�      ��  �@�Cursor$
 Hand��6/   �� 5�       � �� �#ADB5BD63    5�      P� 5�       �@J�IsMouseOver . . �5�      ��  � �� �#dee2e66;   5�      5�      ��5�      �� 5�      " ��6"   .R�5�      �� $��90��6   $��35��6(   $ 10��64   #��z��6F   ._�5�      �� $��Center=�6+   $��Center=�6H   5�      5�      5�      5�      5�      