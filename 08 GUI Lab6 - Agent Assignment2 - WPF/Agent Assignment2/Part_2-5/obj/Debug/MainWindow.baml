   M S B A M L   `   `   `  ����  Part_2-5    Part_2_5.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$$  Agent Assignment2 Lab2 to Lab5��5   
   $	��350��61   $	��650��6>    � 	Resources 5         btnStyle �� 
TargetType �� Property �� Value  tbxStyle  myBrush �� Color  	TileBrush j� TileMode j� ViewportUnits j� Viewport	 �� Opacity��%5  &	        &	 n     '�� 
    &	 �    &	 �    �� 5   
   " ��6!   .�5      ��  � ��$ 20(�6)   �� 5       � ��$
 Bold"�6+   5	      �� 5
   
   " ��6!   .�5      ��  � ��$
 Bold"�6+   �� 5       � ��$ Center=�62   �� 5       � ��$ 0,10,0,0q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$
 Bold"�6+   �� 5       �@�ToolTip$ tt��6(   5      �� 5   
   $ Bisque��6*   �� 5   
   $
 Tile=�6'   $ Absolute=�5      $ 	0 0 32 32�6*   $		 0.3Q�5      6!   5      .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��_File��6   .@�5      m� 
 m� Command$	
 New��6   m� 5      $
 Delete��6   m� 5      $

 Open��6   m� 5      $

 Save��6   m� 5      $
 SaveAs��6   m� 5       $
 Close��6   $
��Exit��6+   5!      m� 5"      $��_Agents��6   .@�5#      m� $
 NotACommand��6   5$      m� 5%      $��Color��6   .@�5&      m� -   5        $��Random Color��5&      61   m� 5'      -   5        $	��Red��5'      6(   m� 5(      -   5        $��Green��5(      6*   m� 5)      -   5        $
��Blue��5)      6)   5*      m� 5+      $��_Edit��6   .@�5,      m� $

 Undo��6   m� 5-      $

 Copy��6   m� 5.      $	
 Cut��6   m� 5/      $
 Paste��6   50      51      `� 52   
   $	��Top=�6    `� 
Background
 ��� �.�53      c� 
�������.@�54      �� -   5        $��btnBack��54      #��[  55      .��67   <6=   �� 56      -   5        $��
btnForward��56      #��[  57      .��67   >6=   �� 58      -   5        $��btnAdd��58      #��[  59      .��67   +6:   �� 5:      -   5        $��btnMinus��5:      #��[  5;      .��67   -6:   �� 5<      $��14(�6   $
��Bold"�6&   $��Center=�68   .��6S   File _Name:6`   �� 5=      -	   5        $��fileName��5=      $	��105��6*   $��Center=�69   6V   5>      c� 5?      
�������.@�5@      �� #��[  6   #���  6;   .��6d   <6j   �� 5A      #��[  6   #���  6;   .��6d   >6j   �� 5B      #��[  6   #���  6;   .��6d   +6g   �� 5C      #��[  6   #���  6;   .��6d   -6g   5D      5E      �� 5F   
   
�������$��Bottom=�6'    � DataContext 5G      ��  �� ElementName$ 	lbxAgents�� �� Path$ SelectedItem�.@�6T   .@�5I      Agents Count: �� .��5J      �� -
   5         ��Name$ 
agentCount��5J      5K      �� 5L      $��Right=�6   .��5M      �� -   5        $ dateTime��5M      5N      5O      � 5P   
   -   5         �Name$ mainGrid��5P      #���  5R       5Q      �� $ 	lbxAgents��$ SelectedItem� � RowDefinitions 5S      �� 5T      $
��Auto �6    �� 5U      $
��Auto �6    �� 5V      $
��Auto �6    �� 5W      $
��Auto �6    �� 5X      $
��Auto �6    �� 5Y      $��15 �6    �� 5Z      $
��Auto �6    �� 5[      $
��Auto �6     � ColumnDefinitions 5]      �� 5^      $��4* �6#   �� 5_      $	��10* �6#   �� 5`      $	��10* �6#   .+�5b      �� $��0��6   $��Id��5c      $��Center=�5d      $��0,10,0,0q�5e      �� 5g      -   5        $��NameId��5g      #��[ 5i      $
��Left=�5j      $��65��5k      $��0��5l      $��1��5m      ��5h      �� $ ID�5m      �� 5o      -   5        $��lblCodeName��5p      $��1��5o      $��CodeName��5q      $��0,10,0,0q�5r      �� 5t      -   5        $��tbxCodeName��5t      #��[ 5v      $��1��5w      $��1��5x      ��5u      �� $ CodeName�5x      �� 5z      $��2��6   $��
Speciality��5{      $��0,10,0,0q�5|      �� 5~      -   5        $��tbxSpeciality��5~      #��[ 5�      $��2��5�      $��1��5�      ��5      �� $ 
Speciality�5�      �� 5�      $��3��6   $��
Assignment��5�      $��0,10,0,0q�5�      �� 5�      -   5        $��tbxAssignment��5�      #��[ 5�      $��3��5�      $��1��5�      $��70��5�      ��5�      �� $ 
Assignment�5�      �� 5�      -   5        $��	lbxAgents��5�       �� DisplayMemberPath$ CodeName��5�       �� SelectedIndex$ 0��5�      $	��175��5�      $��3��5�      $��9��5�      $��	20,0,10,0q�5�      5�      5�      5�      