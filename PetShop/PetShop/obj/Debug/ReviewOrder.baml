   M S B A M L   `   `   `  ����B  >PetShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PetShopPetShop  5   	       PetShop.ReviewOrder   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:PetShop     5   	     � Title$.  (Review Order - Freshwater Snail Emporium��5   	   $	��540��6:   $	��430��6G    � 	Resources 5	         PetTemplate � ColumnDefinitions �� Orientation �� TextAlignment �� TextWrapping��%k  &	        �� 5
   
   .R�5      �  5      �� 5      $
��auto �6'   �� 5      $	��130 �6'   �� 5      $��40 �6'   �� 5      $��70 �6'   �� 5      $��35 �6'   .+�5      �� $��0��6   $��0,0,5,5q�6E   $	��100��6V   $��77��6b   ��6(   �� *	ImagePath+6b   �� 5      $��1��6   $
��Wrap=�6B   $
��Left=�6V   $
��Bold"�6q   ��6,   �� *Name+6q   �� 5      $��2��6   $ 
Horizontal=�6-   $��Right=�6F   .+�5      �� $��$��6    $ Right=�6)   �� 5      $ Right=�67   ��6    �� *Price+67   5      �� 5      $��3��6   $��20��6H   $��40��6T   $	��Top=�6_   $��5,0,5,0q�6w   $
 Wrap=�6�   {�. ��6*   �� *purchasedAmt+6�   �� 5      $��4��6   $ 
Horizontal=�6-   $��Right=�6F   .+�5      �� ��6    �� *total+5      5      5      5      .��5!      �� .+�5"   
   �� $ 
Horizontal=�6   .+�5#      �� $
��Bold"�6   $
��Item��6&   �� 5$      $
��Bold"�6   $��Price��6&   $��	214,0,0,0q�66   �� 5%      $
��Bold"�6   $
��Qty.��6&   $��10,0,0,0q�65   �� 5&      $
��Bold"�6   $��Total��6&   $��25,0,0,0q�66   5'      �� 5(   
   #��[  6?   $
��Auto=�5)      $	��400��6B   ��5(      �� *Parent.CartContents+5)   B   �� 5*   
   $ 
Horizontal=�6   $��Right=�6.   .+�5+      �� $��Total��6   $��25��6$   $��75��60   �� 5,      $��25��6A   $��50��6M   $��0,0,23,0q�6X   $��Right=�6j   ��6   �� *Parent.TotalCostToString+6j   5-      �� 5.   
   $��Update Order��6   $��Right=�6(   $��25��6D   $	��125��6P   $��	0,10,28,0q�6\   ��6o   �� *UpdateCartCommand+5/      50      