   M S B A M L   `   `   `  ����B  >PetShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PetShopPetShop  5          PetShop.PetDisplay   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:PetShop     5      
�������
����� �$��10q�5	   =     � DataContext  5
         PetShop.PetDisplayVM  5   
    � 	Resources 5         PetTemplate �� Orientation ��Name��%#  &	        �� 5   
   .R�5      �� $��5,0,0,0q�6   .+�5      �� $��0,5,0,5q�65   $
��Left=�6F   $	��140��6a   $	��108��6m   ��6   �� *	ImagePath+6m   �� 5      $��14(�62   $
��Bold"�6@   $
��Wrap=�6R   ��6   �� *Name+6R   �� 5      $ 
Horizontal=�6   .+�5      �� $��Price: $��6    �� 5      ��6    �� *Price+5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Stock: ��6    �� 5      $ 
StockLabel��67   ��6    �� *Stock+67   5      5      5      5      .��5      �� -   5         ��Name$ 
PetListBox��5      #��[  6M   ��6#   �� *Pets+ �� SelectedItem 5       �� *SelectedPet+��5!   
   �� 5"      .R�5#      A�  A� Columns$ 4��6   5$      5&      5'      