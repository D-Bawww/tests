   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5   	       WpfApp2.PopWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp2     5   	   -   5          �Name$
  wind��5   #    � Title$' !Аренда транспорта��6	   $	��450��61   $	��700��6>   
�������.��5	      � $��5,5,5,5q�6    � RowDefinitions 5
   
   �� 5      $	��12* �6   �� 5      $
��205* �6   .+�5   
   �� -   5         ��Name$
 text��5      $��Арендодатели��6"   
������� �� TextAlignment$ Center=�6N   
���   �$��14(�6x   �� 5   
   $��1q�6   
���   �6%   �� 5   
   -   5         ��Name$
 list��5      
���   �$��1��64   $y�	Collapsed=�6A   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$
 grid��5      $��1��6!   
�������
���   �   GridLinesVisibility$ Vertical=�6U      VerticalGridLinesBrush
 �   �	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$! Номер телефона��6%      
Foreground
 �   �   Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6v   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   ��  �� Path$ phone_number�6v     5      $ 
Марка��6%   
 �   �$ 1* 6e    64   �� $
 mark�6e     5      $ Модель��6%   
 �   �$ 1* 6g    65   �� $ model�6g     5      $ Год��6%   
 �   �$ 1* 6a    62   �� $ yy�6a     5      $% Стоимость аренды��6%   
 �   �$ 1* 6q    6?   �� $ price�6q     5      $ Срок аренды��6%   
 �   �$ 1* 6m    6:   �� $ period�6m   5      5      5      