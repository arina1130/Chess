   M S B A M L   `   `   `  ����@  <Chess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChessChess  5          Chess.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Chess     5      1muxc using:Microsoft.UI.Xaml.Controls     5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  65   Q MControlzEx, Version=4.0.0.0, Culture=neutral, PublicKeyToken=69f1c32f803d307e
controlzexurn:controlzex  5      $	��600��5
   0     N� Title$  Settings��5       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius �� Triggers	 P� Property
 P� Value   SettingText��%g  '��       '�� �    &	  B    'd� 9    �� 5   
   " ��6   .�5      ��  � �� �#46211A6+   �� 5       � �� �#C9A66B6+   �� 5       � ��$ 16(�6)   �� 5       � ��$	 150��6&   �� 5       � ��$ 40��6'   �� 5       � ��$% Gill Sans MT Ext Condensed Bold)�6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��5   !   ._�5      �� #��z��6/   #��z��5   /   5      5      5      �� 5       �@J�Effect 5         5!      $ 10Q�6'   5#       5$      P� 5%      	 �@J�IsMouseOver
 . . �5&      ��  � �� �	#FF350D0563   5'      5)      �� 5*   
   " ��6   .�5+      ��  � �� �#C9A66B6+   �� 5,       � ��$
 Bold"�6+   �� 5-       � ��$ Yandex-UI-Icons-Private)�6+   5.      �� 5/   
   " ��6$   .�50      ��  � ��$
 Left=�64   �� 51       � ��$ 20(�6)   �� 52       � �� �#C9A66B6+   �� 53       � ��$
 Bold"�6+   �� 54       � ��$ Yandex-UI-Icons-Private)�6+   55      �� 56   
   " d�6   .�57      ��  � �� �#C9A66B6+   �� 58       � ��$
 Bold"�6+   �� 59       � ��
 ���� 6+   �� 5:       � ��
 ���� 6,   �� 5;       � ��$ 30��6'   �� 5<       � ��$ 80��6&   �� 5=       � ��$ 18(�6)    5>      P� 5?      	 �@d�	IsChecked$

 TrueY�6/   . �5@      ��  � ��$ Вкл.��60   5A      P� 5B      	 �@d�	IsChecked$
 FalseY�6/   . �5C      ��  � ��$ 	Выкл.��60   5D      5F      5G      .��5H      � .+�5I   
   �� -   5         ��Name$
 back��5I      $$��Вернуться в меню��6   $
��Left=�6:   $��	10,10,0,0q�6U   $	��Top=�6h   6�   �� 5J   
   $��0,20,0,0q�6   $��26(�6&   $	��400��64   $��45��6@   $��Center=�6L   $��Настройки��6i    �� TextAlignment$ Center=�6z   $	��Top=�6�   �� 5K   
   #��[  6   $
��Left=�69   $��200,139,0,0q�6T   $��Таймер:��6i   $	��Top=�6x   d� 5L   
   -   5         d�Name$ 	isTimerOn��5L      $��510,137,200,433q�6C    d� 	IsChecked$ FalseY�6\   �� 5M   
   #��[  6   $
��Left=�69   $��200,181,0,0q�6T   $��Время:��6i   $	��Top=�6�   {�6w   �� *	IsChecked+ �� ElementName$ timer��6�   d� 5N   
   -   5        $ isVisibleMoves��5N      $��510,218,200,352q�6/   $
 TrueY�6H   �� 5O   
   #��[  6   $
��Left=�69   $��200,223,0,0q�6T   $7��1Видимость доступных ходов:��6i   $	��Top=�6�   {�6�   �� *	IsChecked+$ timer��6�   *  $MahApps.Metro.Controls.NumericUpDown  5P   
   -   5        $��Minute��5P      $��510,175,200,390q�6*      Minimum$ 0Q�6z      Maximum$ 60Q�6�      Value$ 15X�6�   $��20(�6�   $��35��6�   
�������$��80��6�   {�6C   �� *	IsChecked+$ 	isTimerOn��6�   5Q      5R      