
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using WebApiProducts.Models;

namespace WebApiProducts.Repository
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly string _connectionString;

        public ProductoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("EcomSurConnection");
        }

        

        public async Task<Products> GetProductById(int id)
        {
            Products product = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("ProductoById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@productoId", id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                product = new Products();
                                product.productoId = reader.GetInt32(reader.GetOrdinal("fiProductoId"));
                                product.producto = reader.GetString(reader.GetOrdinal("fcProducto"));
                                product.cantidadProducto = reader.GetInt32(reader.GetOrdinal("fiCantidadProducto"));
                                product.precio = (float)reader.GetDouble(reader.GetOrdinal("fdPrecio"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto", ex);
            }

            return product;
        }


        public async Task<List<Products>> GetProducts()
        {
            List<Products> products = new List<Products>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                    throw;
                }

                using (SqlCommand command = new SqlCommand("ListProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        try
                        {
                            while (await reader.ReadAsync())
                            {
                                Products product = new Products();
                                product.productoId = reader.GetInt32(reader.GetOrdinal("fiProductoId"));
                                product.producto = reader.GetString(reader.GetOrdinal("fcProducto"));
                                product.cantidadProducto = reader.GetInt32(reader.GetOrdinal("fiCantidadProducto"));
                                product.precio = (float)reader.GetDouble(reader.GetOrdinal("fdPrecio"));

                                products.Add(product);
                            }
                        }
                        catch (Exception ex)
                        {
                          
                            Console.WriteLine($"Error al leer los resultados: {ex.Message}");
                            throw;
                        }
                    }
                }
            }

            return products;
        }

        public async Task<bool> InsertProduct(Products product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("InsertProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Product", SqlDbType.VarChar).Value = product.producto;
                        command.Parameters.Add("@Amount", SqlDbType.Int).Value = product.cantidadProducto;
                        command.Parameters.Add("@Price", SqlDbType.Money).Value = product.precio;
                        var id = await command.ExecuteScalarAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateProduct(Products product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("UpdateProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@productoId", product.productoId);
                        command.Parameters.AddWithValue("@Product", product.producto);
                        command.Parameters.AddWithValue("@cantidadProducto", product.cantidadProducto);
                        command.Parameters.AddWithValue("@precio", product.precio);
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteProductById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@productoId", id);
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo del error
                Console.WriteLine($"Error al eliminar el producto con Id {id}: {ex.Message}");
                return false;
            }
        }


       
    }
}
