   M S B A M L   `   `   `  ����H  DClothingStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClothingStoreClothingStore  5           ClothingStore.Registration   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:ClothingStore     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  Registration��5
      $
��1080��5      $
��1920��5      .��5      � 
�������.+�5   
   �� -   5        $*��$Зарегистрироваться��5      $��Center=�6.   $��	0,600,0,0q�6K   $	��Top=�6^   $	��200��6v   $��0q�6�   $��
Gosha Sans)�6�   $��Light"�6�   $��20(�6�   ���	#FF343A40 J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 7Q�6#      Color$ 	#FFC5C5C5��62      ShadowDepth$ 3Q�6D      	Direction$	 270Q�6T      Opacity$	 0.8Q�6d      RenderingBias$ Quality=�6r   ��5      �� 5       �� 
TargetType" ��6   .�5      �� 	 �� Property	 �@�Cursor
 �� Value$

 Hand��6/   �� 5      	 � ��
 �#ADB5BD63    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      �� 	 � ��
 �#dee2e66;   5      5      ��5!      �� 5"       �� 
TargetType" ��6"   .R�5#      �� $	��200��6   $��50��6)    �� CornerRadius$ 10��65   #��z��6G   ._�5$      �� $��Center=�6+   $��Center=�6H   5%      5&      5(      �� 5)   
   -   5         ��Name$ loginBox��5)      $��0q�6%   $��Center=�69   $��50��6V   $��	0,300,0,0q�6b    �� TextWrapping$
 Wrap=�6u   $��Пароль��6�   $	��Top=�6�   $	��300��6�   $��
Gosha Sans)�6�   $��Light"�6�   $��20(�6�   ���	#FFCED4DA � UseLayoutRounding .  .��6%  ��5*      �� 5,      " ��6   .�5.      �� 	 � ��
 5/      �� 50      " ��6.   .R�51   "   �� $ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   54   #   55      59      5;      .��5<      ��5>      �� 5?      " ��6"   .R�5@      � .+�5A      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�5B      �� $��Center=�6/   $��Center=�6L   5D      �� 5E       ��Name$ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5F      5H      .��5I       5J      �� 5K   
   -   5        $ passwordBox��5K      $��0q�6'   $��Center=�6;   $��50��6X   $��	0,230,0,0q�6d   $
 Wrap=�6w   $��
Логин��6�   $	��Top=�6�   $	��300��6�   $��
Gosha Sans)�6�   $��Light"�6�   $��20(�6�   ���	#FFCED4DA .  .��6&  ��5L      �� 5N      " ��6   .�5P      �� 	 � ��
 5Q      �� 5R      " ��6.   .R�5S   "   �� $ 10��6)   ���	#FFCED4DA$��Center=�6R   $��Center=�6o   5V   #   5W      5[      5]      .��5^      ��5`      �� 5a      " ��6"   .R�5b      � .+�5c      �� $	��300��6!   $��50��6-   $ 10��69   #��z��6K   ._�5d      �� $��Center=�6/   $��Center=�6L   5f      �� 5g      $ PART_ContentHost��6'   ��.  #��z��6i   #��z��6�   $��0q�6�   $��5,10,1,1q�6�   ��6S   Z� 6�   5h      5j      .��5k       5l      �� 5m   
   -   5         ��Name$ roleBox��5m      $��Center=�6$   $��	0,350,0,0q�6A   $	��Top=�6T   $	��300��6l   $��65��6x   $��0q�6�   ���	#FFCED4DA    MaterialDesignFilledComboBox#���   6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Роль��6�   $��
Gosha Sans)�6  $��Light"�6$  $��20(�67  5o      �� 5p   
   $��
Gosha Sans)�6   $��Light"�6,   $��Регистрация��6?   $��Center=�6R   $��841,103,844,0q�6o   $	��Top=�6�   $��40(�6�   ���	#FF343A406�   �� 5q   
   -   5         ��Name$ 
warningBox��5q      $
��Left=�6(   $��919,455,0,0q�6C   $
��Wrap=�6X   $�� ��6l   $	��Top=�6t   
���  ��6�   5s      5t      