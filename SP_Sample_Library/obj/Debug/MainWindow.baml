   M S B A M L   `   `   `  ����L  HSP_Sample_Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SP_Sample_LibrarySP_Sample_Library  5   	   "    SP_Sample_Library.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:SP_Sample_Library     5   	   -   5          �Name$  SharePoint_Sample��5   	    � Title$ 
MainWindow��5   	   $��365.332��6   $	��525��6-   .��5	      � $��0,0,2,0q�6    � ColumnDefinitions 5
   
   �� 5      $��6* �6   �� 5      $	��41* �6   .+�5   
   �� -   5         ��Name$ label��5      $��Server:��6   $
��Left=�61   $��	36,43,0,0q�6L   $	��Top=�6_   $��14(�6w   $��2��6�   �� 5   
   -   5         ��Name$ 	txtServer��5      $
��Left=�6%   $��28��6@   $��	59,43,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��341��6�   $��16(�6�   $��1��6�   �� 5   
   -   5        $ label1��5      $��User:��6    $
��Left=�60   $��	36,89,0,0q�6K   $	��Top=�6^   $��14(�6v   $��2��6�   �� 5   
   -   5        $ txtUserName��5      $
��Left=�6'   $��28��6B   $��	59,89,0,0q�6N   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��341��6�   $��14(�6�   $��1��6�   �� 5   
   -   5        $ label2��5      $��	Password:��6    $
��Left=�64   $��
38,138,0,0q�6O   $	��Top=�6c   $��81��6{   $��14(�6�   $��2��6�   �� 5   
   -   5         ��Name$ cmbSites��5      $
��Left=�6%   $��
59,180,0,0q�6@   $	��Top=�6T   $	��341��6l   $��28��6x   $��14(�6�   $��1��6�   �� 5   
   -   5        $ label3��5      $��Site:��6    $
��Left=�60   $��
37,186,0,0q�6K   $	��Top=�6_   $��14(�6w   $��2��6�   �� 5   
   -	   5        $ label4��5      $��Library��6    $
��Left=�62   $��
37,227,0,0q�6M   $	��Top=�6a   $��14(�6y   $��2��6�   �� 5   
   -
   5        $ 
cmbLibrary��5      $
��Left=�6'   $��
59,227,0,0q�6B   $	��Top=�6V   $��28��6n   $	��341��6z   $��14(�6�   $��1��6�   G� 5   
   -   5         G�Name$ password��5      $
��Left=�6(   $��
59,133,0,0q�6C   $	��Top=�6W   $	��341��6o   $��28��6{   $��14(�6�   $��1��6�   5      5      