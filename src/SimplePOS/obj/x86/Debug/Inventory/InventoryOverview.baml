   M S B A M L   `   `   `  ����  	SimplePOS+    %SimplePOS.Inventory.InventoryOverview   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Inventurübersicht��5   	   $	��300��6#   $	��477��60    � WindowStartupLocation$ CenterOwner=�6<    � 
ResizeMode$ NoResize=�6`   .��5      � $
��Left=�6   $	��467��6&   $��0q�62    � RowDefinitions 5   
   �� 5      $
��266* �6   �� 5      $��3* �6   �� 5	      $��3* �6    � ColumnDefinitions 5   
   �� 5      $
��287* �6   �� 5      $	��12* �6   �� 5      $
��Auto �6   $��86��6,   .+�5   
   �� -   5         ��Name$ Button1��5      $
��Left=�6"   $	��Top=�6=   $��75��6U   $��53��6`   $��0,10,0,0q�6�   $��2��6�   .��5      �� �� 6   $�Differenzen��6   �� 62   �� 6>   $�	ausbuchen��6B   6V   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	DataGrid1��5   '   $��
10,10,0,10q�6      
IsReadOnly .    CanUserSortColumns .  	   FrozenColumnCount$	 1��6g   5      5      