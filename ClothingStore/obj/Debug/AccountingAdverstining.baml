   M S B A M L   `   `   `  ����H  DClothingStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClothingStoreClothingStore  5      *    $ClothingStore.AccountingAdverstining   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      'localclr-namespace:ClothingStore  5        N� Title$  AccountingAdverstining��5
      $
��1080��5      $
��1920��5      .��5      � 
�������.+�5   
   �� $��
Gosha Sans)�6   $��Light"�6,   $!��Расчёт рекламы��6?   $��Center=�6U   $��810,112,805,0q�6r   $	��Top=�6�   $��40(�6�   ���	#FF343A406�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ accountingGrid��5      $
��Left=�6+   $	��608��6F   $��
52,409,0,0q�6S   $	��Top=�6g   $
��1806��6      AutoGenerateColumns .  ���#DEE2E6$��0q�6�      MaterialDesignDataGrid#���   6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *DateofAdverstining+  5      $ Скидка��6%    65   �� *Stock+  5      $ Стоимость��6%    68   �� *Calculation+4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      ��  ��Name$ Delete��6%   -   5        $��Удалить��5   5   ���#CED4DA���#CED4DA
���   �6�   5      5      5       (  "System.Windows.Controls.DatePicker  5!   
   -   5          Name$ datePick��5!      $
��Left=�6'   $��
52,367,0,0q�6B   $	��Top=�6V   $��0q�6n   ���	#FFCED4DA6�   �� 5"   
   -   5        	 ��Name$	 boxStock��5"      $
��Left=�6%   $��191,352,0,0q�6@   $	��Top=�6U   $	��120��6m   $��0q�6y   ���	#FFCED4DA   MaterialDesignFilledComboBox#���  6�   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Скидка��6�   �� 5#   
   -   5        $	 boxCalculation��5#      $
��Left=�6+   $��339,352,0,0q�6F   $	��Top=�6[   $	��120��6s   $��0q�6   ���	#FFCED4DA#���  6�   $
 Стоимость��6�   �� 5$   
   -   5        $ addBtn��5$      $��Добавить��6!   $
��Left=�64   $��482,361,0,0q�6O   $	��Top=�6d   $��90��6|   $��
Gosha Sans)�6�   $��Light"�6�   $��10(�6�   ���	#FF343A40 J� Effect 5%      3  -System.Windows.Media.Effects.DropShadowEffect  5&         
BlurRadius$ 7Q�6#      Color$ 	#FFC5C5C5��62      ShadowDepth$ 3Q�6D      	Direction$	 270Q�6T      Opacity$	 0.8Q�6d      RenderingBias$ Quality=�6r   ��5(      �� 5)       �� 
TargetType" ��6   .�5*      ��  �� Property �@�Cursor �� Value$
 Hand��6/   �� 5+       � �� �#ADB5BD63    �� Triggers 5,      P� 5-       P� Property �@J�IsMouseOver P� Value . . �5.      ��  � �� �#dee2e66;   5/      52      ��55      �� 56       �� 
TargetType" ��6"   .R�57      �� $��90��6   $��35��6(    �� CornerRadius$ 10��64   #��z��6F   ._�58      �� $��Center=�6+   $��Center=�6H   59      5:      5<      �� 5=   
   -   5        $	 	filterBox��5=      $
��Left=�6&   $��1737,348,0,0q�6A   $	��Top=�6W   $	��121��6o   $��0q�6�   ���	#FFCED4DA#���  6�   �� 5>   
   -   5         ��Name$ 
warningBox��5>      $��Center=�6(   $��
0,-650,0,0q�6E   $
��Wrap=�6Y   $�� ��6m   $��20(�6u   
���  �� �� TextAlignment$ Center=�6�   $��Center=�6�   �� 5?   
   -	   5        $ btnReset��5?      $��Восстановить��6#   $
��Left=�6:   $��1764,302,0,0q�6U   $	��Top=�6k   $��90��6�   $��
Gosha Sans)�6�   $��Light"�6�   $��10(�6�   ���	#FF343A40 5@        5A      $ 7Q�6#   $ 	#FFC5C5C5��62   $ 3Q�6D   $	 270Q�6T   $	 0.8Q�6d   $ Quality=�6r   ��5C      �� 5D      " ��6   .�5E      ��  �@�Cursor$
 Hand��6/   �� 5F       � �� �#ADB5BD63    5G      P� 5H       �@J�IsMouseOver . . �5I      ��  � �� �#dee2e66;   5J      5M      ��5P      �� 5Q      " ��6"   .R�5R      �� $��90��6   $��35��6(   $ 10��64   #��z��6F   ._�5S      �� $��Center=�6+   $��Center=�6H   5T      5U      5W      5Y      5Z      