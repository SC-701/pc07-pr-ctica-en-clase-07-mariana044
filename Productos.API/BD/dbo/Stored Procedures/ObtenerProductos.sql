CREATE PROCEDURE [dbo].[ObtenerProductos]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT Producto.Id, Producto.IdSubCategoria, Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.CodigoBarras, Categorias.Nombre AS Categorias, SubCategorias.Nombre AS Subcategorias
FROM     Producto INNER JOIN
                  Subcategorias ON Producto.IdSubcategoria = Subcategorias.Id INNER JOIN
                  Categorias ON Categorias.Id = SubCategorias.IdCategoria
END