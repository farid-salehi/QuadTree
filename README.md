# QuadTree

This project is designed and developed for storing and retrieving locations based on a Geographic coordinate system(Latitude and longitude).

Quadtree algorithm is used in this project to provide an optimized functionality for searching and finding locations.

### APIS:
This Web API project contains two APIs:
1.  GET - API/QuadTree/Search
Find locations at a certain distance from a specific location, order by distance(nearest first). 
2.  POST - /API/QuadTree/Insert
Insert Locations

-   This web API project is powered by swagger.

### Boundary:
The default(earth) boundary is set on the  appsettings.json file, and it's configurable:
(90, -180), (90, 180), (-90,-180), (-90,180) 
-   The Boundary and the main tree will be created at first run automatically.
### Capacity:
- The default capacity is set on the appsettings.json file, and it's configurable.

### Dependencies:
##### .Net 5
##### SQL Server - EntityFrameworkCore 
  
  
