## Miimo Test Unity Project
Thank you for the opportunity to demonstrate my skills through this test Unity project for Miimo. This project showcases my ability to develop and test features using Unity.

This Unity project is a technical test created as part of my application for a position at Miimo. It includes a range of features and tests to demonstrate my proficiency with Unity and game development concepts.

The project fulfills the following requirements:

## Requirements
The project fulfills the following requirements:

    Unity Version: Use Unity 2021.1.x version.
    Support for Different Portrait Resolutions:
        Supports various portrait resolutions, including 700px x 1280px, 1200px x 2400px, and others.
    Support for Variation of Grid Sizes:
        Supports multiple grid sizes, including 8x8, 12x10, and others.
    Normal Piece:
        Contains 4 colors: Red, Green, Blue, and Yellow.
        When two or more connected pieces of the same color are tapped, those pieces are destroyed.
    Special Pieces:
        Bomb Piece:
            Appears when 6 or more pieces are destroyed.
            When tapped, it destroys all pieces on the X and Y axes.
        Disco Piece:
            Appears when 10 or more pieces are destroyed.
            When tapped, it destroys all pieces on the grid with the same color as the Disco piece.


## Implemented Features
    In this project, I have implemented the following features to meet the requirements:

    Portrait Resolution Support:
        Configured the game to support various portrait resolutions by "ResponsiveCamera.cs"
        Ensured UI elements and gameplay adapt to different screen sizes.

    Grid Size Variation:
        Implemented functionality to dynamically support different grid sizes, including 8x8 and 12x10.
        Utilized a script named GridManager with adjustable properties width and height to easily configure the grid size.
        The GridManager script allows for flexible grid configurations, enabling the game to adapt to any specified grid dimensions through visual scripting.

    Normal Piece:
        Created pieces with four distinct colors.
        Implemented tap functionality to destroy connected pieces of the same color.

    Special Pieces:
        Bomb Piece:
            Appears when 6 or more pieces are destroyed.
            Destroys all pieces on the X and Y axes when tapped.
        Disco Piece:
            Appears when 10 or more pieces are destroyed.
            Destroys all pieces of the same color on the grid when tapped.

    Additional Features:
        Implemented a score system to track player performance.
        Each piece destroyed adds points to the score:
            Normal pieces: 100 points per piece.
            Bomb pieces: 200 points per piece.
            Disco pieces: 100 points per piece.

## Notes
    Unity Version: This project was developed using Unity 2021.1.5f1. Ensure you have the correct version to avoid compatibility issues.

## Contact
If you have any questions or need further information, please feel free to contact me:

Name: Arayan Khumngoen
Email: arayan.khumngoen@gmail.com
Thank you for reviewing my test Unity project for Miimo. I look forward to discussing it further with you!