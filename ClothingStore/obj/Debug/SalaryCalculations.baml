   M S B A M L   `   `   `  ����H  DClothingStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClothingStoreClothingStore  5      &     ClothingStore.SalaryCalculations   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      'localclr-namespace:ClothingStore  5        N� Title$  SalaryCalculations��5
      #��Z��5      #��Z��5      .��5      � 
�������.+�5   
   �� $��
Gosha Sans)�6   $��Light"�6,   $#��Расчёт зарплаты��6?   $��Center=�6V   $��804,100,797,0q�6s   $	��Top=�6�   $��40(�6�   ���	#FF343A406�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
salaryGrid��5      $
��Left=�6'   $	��521��6B   $��
43,518,0,0q�6O   $	��Top=�6c   $
��1831��6{   $��0q�6�      MaterialDesignDataGrid#���   6�   ���#DEE2E6   AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$% Часов отработано��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *HoursWorked+  5      $ Расписание��6%    69   �� *
Schelude+  5      $! Стоимость часа��6%    6=   �� *
CostOfHour+4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      ��  ��Name$ Delete��6%   -   5        $��Удалить��5   5   ���#CED4DA���#CED4DA
���   �6�   5      5      5      �� 5    
   -   5         ��Name$ hoursBox��5       $��0q�6$   $��50��68   $��43,448,1577,0q�6D   	 �� TextWrapping$
	 Wrap=�6[   $%��Часов отработано��6o   $	��300��6�   $��
Gosha Sans)�6�   $��Light"�6�   ���	#FFCED4DA
 �� TextAlignment$

 Left=�6�   $	��Top=�6�   $��Center=�6  $��20(�6  .��6Z  ��5!      �� 5#       �� 
TargetType" ��6   .�5%      ��  �� Property � �� �� Value 5&      �� 5'       �� 
TargetType" ��6.   .R�5(   "   ��  �� CornerRadius$ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   5+   #   5,      50      52      .��53      ��55      �� 56      " ��6"   .R�57      � .+�58      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�59      �� $��Center=�6/   $��Center=�6L   5;      �� 5<       ��Name$ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5=      5?      .��5@       5A      �� 5B   
   -   5        $ scheludeBox��5B      $��0q�6'   $��50��6;   $��367,448,1253,0q�6G   $
	 Wrap=�6_   $��Расписание��6s   $	��300��6�   $��Dubai)�6�   ���	#FFCED4DA$

 Left=�6�   $	��Top=�6�   $��Center=�6�   $��20(�6  .��6  ��5C      �� 5E      " ��6   .�5G      ��  � �� 5H      �� 5I      " ��6.   .R�5J   "   �� $ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   5M   #   5N      5R      5T      .��5U      ��5W      �� 5X      " ��6"   .R�5Y      � .+�5Z      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�5[      �� $��Center=�6/   $��Center=�6L   5]      �� 5^      $ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5_      5a      .��5b       5c      �� 5d   
   -   5        $ costBox��5d      $��0q�6#   $��50��67   $��691,448,929,0q�6C   $
	 Wrap=�6Z   $!��Стоимость часа��6n   $	��300��6�   $��Dubai)�6�   ���	#FFCED4DA$

 Left=�6�   $	��Top=�6�   $��Center=�6�   $��20(�6  .��6>  ��5e      �� 5g      " ��6   .�5i      ��  � �� 5j      �� 5k      " ��6.   .R�5l   "   �� $ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   5o   #   5p      5t      5v      .��5w      ��5y      �� 5z      " ��6"   .R�5{      � .+�5|      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�5}      �� $��Center=�6/   $��Center=�6L   5      �� 5�      $ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5�      5�      .��5�       5�      �� 5�   
   -   5        $ addBtn��5�      $��Добавить��6!   $
��Left=�64   $��1019,458,0,0q�6O   $	��Top=�6e   $��90��6}   $��
Gosha Sans)�6�   $��Light"�6�   $��10(�6�   ���	#FF343A40 J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         
BlurRadius$ 7Q�6#      Color$ 	#FFC5C5C5��62      ShadowDepth$ 3Q�6D      	Direction$	 270Q�6T      Opacity$	 0.8Q�6d      RenderingBias$ Quality=�6r   ��5�      �� 5�      " ��6   .�5�      ��  �@�Cursor$
 Hand��6/   �� 5�       � �� �#ADB5BD63    �� Triggers 5�      P� 5�       P� Property �@J�IsMouseOver P� Value . . �5�      ��  � �� �#dee2e66;   5�      5�      ��5�      �� 5�      " ��6"   .R�5�      �� $��90��6   $��35��6(   $ 10��64   #��z��6F   ._�5�      �� $��Center=�6+   $��Center=�6H   5�      5�      5�      �� 5�   
   -   5         ��Name$ 	filterBox��5�      $
��Left=�6&   $��1688,448,0,0q�6A   $	��Top=�6W   $	��186��6o   $��45��6�   $��0q�6�      MaterialDesignFilledComboBox#���  6�   ���	#FFCED4DA)  #MaterialDesignThemes.Wpf.HintAssist   Hint$/ )Фильтрация расписания��6  �� 5�   
   -   5         ��Name$ 
warningBox��5�      $��Center=�6(   $��
0,-650,0,0q�6E   $
��Wrap=�6Y   $�� ��6m   $��20(�6v   
���  �� �� TextAlignment$ Center=�6�   $��Center=�6�   �� 5�   
   -	   5        $ btnReset��5�      $��Восстановить��6#   $
��Left=�6:   $��1784,414,0,0q�6U   $	��Top=�6k   $��90��6�   $��
Gosha Sans)�6�   $��Light"�6�   $��10(�6�   ���	#FF343A40 5�        5�      $ 7Q�6#   $ 	#FFC5C5C5��62   $ 3Q�6D   $	 270Q�6T   $	 0.8Q�6d   $ Quality=�6r   ��5�      �� 5�      " ��6   .�5�      ��  �@�Cursor$
 Hand��6/   �� 5�       � �� �#ADB5BD63    5�      P� 5�       �@J�IsMouseOver . . �5�      ��  � �� �#dee2e66;   5�      5�      ��5�      �� 5�      " ��6"   .R�5�      �� $��90��6   $��35��6(   $ 10��64   #��z��6F   ._�5�      �� $��Center=�6+   $��Center=�6H   5�      5�      5�      5�      5�      