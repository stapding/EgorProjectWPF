
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Rus
{
    public class JsonRepository
    {

        public const String FILE_NAME = "user.json";
        public const String PRODUCT_FILE_NAME = "product.json";
        public const String CATEGORY_FILE_NAME = "category.json";
        public const String INDEXES_FILE_NAME = "index.json";


        public static bool existsProductByCategoryId(long categoryId)
        {
            if (!new FileInfo(PRODUCT_FILE_NAME).Exists) return false;
            else
            {
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Open))
                {
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(fs);
                    foreach(var product in products)
                    {
                        if(product.Category.Id==categoryId) return true;
                    }
                }
            }
            return false;
        }


        public static int getIdByKey(string key)
        {
            int id = 0;
            if (!new FileInfo(INDEXES_FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(INDEXES_FILE_NAME, FileMode.Create))
                {
                    
                    List<Index> indexes = new List<Index>();
                    indexes.Add(new Index(PRODUCT_FILE_NAME,1));
                    indexes.Add(new Index(CATEGORY_FILE_NAME, 1));
                    indexes.ForEach(v =>
                    {
                        if(v.key == key)
                        {
                            id = v.value;
                            v.value += 1;
                        }
                    });
                    JsonSerializer.Serialize(fs, indexes);
                }
            } else
            {
                List<Index> indexes = null;
                using (FileStream fs = new FileStream(INDEXES_FILE_NAME, FileMode.Open))
                {
                    indexes = JsonSerializer.Deserialize<List<Index>>(fs);

                    Index index = indexes.Where(v => v.key==key).SingleOrDefault();
                    id = index.value;
                    index.value += 1;
                }
                using (FileStream fs = new FileStream(INDEXES_FILE_NAME, FileMode.Truncate))
                {
                    JsonSerializer.Serialize(fs, indexes);
                }
            }
            return id;
        }

        public static List<User> findAll()
        {
            if (new FileInfo(FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
                {
                    return JsonSerializer.Deserialize<List<User>>(fs);
                }
            }
            return null;
        }

        public static List<Product> findAllProducts()
        {
            if (new FileInfo(PRODUCT_FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Open))
                {
                    List<Category> categories = findAllCategories();   
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(fs);
                    products.ForEach((v) =>
                    {
                        v.Category = categories.Where(c => c.Id == v.Category.Id).SingleOrDefault();
                    });
                    return products;
                }
            }
            return null;
        }

        public static List<Category> findAllCategories()
        {
            if (new FileInfo(CATEGORY_FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Open))
                {
                    return JsonSerializer.Deserialize<List<Category>>(fs);
                }
            }
            return null;
        }

        public static void deleteByEmail(string email)
        {
            if (new FileInfo(FILE_NAME).Exists)
            {
                List<User> list = null;
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<User>>(fs);
                }
                int index = list.FindIndex(v => v.Email.Equals(email));
                list.RemoveAt(index);
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Truncate))
                {
                    JsonSerializer.Serialize(fs, list);
                }
            }
        }

        public static void deleteById(long id)
        {
            if (new FileInfo(PRODUCT_FILE_NAME).Exists)
            {
                List<Product> list = null;
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<Product>>(fs);
                }
                int index = list.FindIndex(v => v.Id == id);
                list.RemoveAt(index);
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Truncate))
                {
                    JsonSerializer.Serialize(fs, list);
                }
            }
        }

        public static void deleteCategoryById(long id)
        {
            if (new FileInfo(CATEGORY_FILE_NAME).Exists)
            {
                List<Category> list = null;
                using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<Category>>(fs);
                }
                int index = list.FindIndex(v => v.Id == id);
                list.RemoveAt(index);
                using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Truncate))
                {
                    JsonSerializer.Serialize(fs, list);
                }
            }
        }

        public static User findByEmail(string email)
        {
            User runner = null;
            if (new FileInfo(FILE_NAME).Exists)
            {
                List<User> list = null;
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<User>>(fs);
                }
                int index = list.FindIndex(v => v.Email.Equals(email));
                if(index>=0)
                {
                    runner = list[index];
                }
            }
            return runner;
        }

        public static Product findById(long id)
        {
            Product product = null;
            if (new FileInfo(PRODUCT_FILE_NAME).Exists)
            {
                List<Product> list = null;
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<Product>>(fs);
                }
                int index = list.FindIndex(v => v.Id == id);
                if (index >= 0)
                {
                    product = list[index];
                    product.Category = findAllCategories().Where(c => c.Id == product.Category.Id).SingleOrDefault();
                }
            }
            return product;
        }

        public static Category findCategoryById(long id)
        {
            Category category = null;
            if (new FileInfo(CATEGORY_FILE_NAME).Exists)
            {
                List<Category> list = null;
                using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<Category>>(fs);
                }
                int index = list.FindIndex(v => v.Id == id);
                if (index >= 0)
                {
                    category = list[index];
                }
            }
            return category;
        }

        public static Category addCategory(Category category)
        {
            if (!new FileInfo(CATEGORY_FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Create))
                {
                    List<Category> list = new List<Category>();
                    category.Id = getIdByKey(CATEGORY_FILE_NAME);
                    list.Add(category);
                    JsonSerializer.Serialize(fs, list);
                    return category;
                }
            }
            else
            {
                List<Category> list = null;
                using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<Category>>(fs);
                }
                Category status = editCategory(category);
                if (status == null)
                {
                    category.Id = getIdByKey(CATEGORY_FILE_NAME);
                    list.Add(category);
                    using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Truncate))
                    {
                        JsonSerializer.Serialize(fs, list);
                    }
                    return category;
                }
                else
                {
                    return status;
                }
            }
            return null;
        }

        public static Category editCategory(Category category)
        {
            Category dbCategory = findCategoryById(category.Id);
            if (dbCategory == null)
            {
                return null;
            }

            dbCategory.Name = category.Name;

            List<Category> list = null;
            using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Open))
            {
                list = JsonSerializer.Deserialize<List<Category>>(fs);
            }
            using (FileStream fs = new FileStream(CATEGORY_FILE_NAME, FileMode.Truncate))
            {
                list.RemoveAll(v => v.Id == dbCategory.Id);
                list.Add(dbCategory);
                JsonSerializer.Serialize(fs, list);
            }
            return dbCategory;
        }

        public static Product addProduct(Product product)
        {
            product.Category = new Category(product.Category.Id);
            if (!new FileInfo(PRODUCT_FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Create))
                {
                    List<Product> list = new List<Product>();
                    product.Id = getIdByKey(PRODUCT_FILE_NAME);
                    list.Add(product);
                    JsonSerializer.Serialize(fs, list);
                    return product;
                }
            }
            else
            {
                List<Product> list = null;
                using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<Product>>(fs);
                }
                Product status = editProduct(product);
                if (status == null)
                {
                    product.Id = getIdByKey(PRODUCT_FILE_NAME);
                    list.Add(product);
                    using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Truncate))
                    {
                        JsonSerializer.Serialize(fs, list);
                    }
                    return product;
                }
                else
                {
                    return status;
                }
            }
            return null;
        }

        public static Product editProduct(Product product)
        {
            Product dbProduct = findById(product.Id);
            if (dbProduct == null)
            {
                return null;
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Category = new Category(product.Category.Id);
            dbProduct.Count = product.Count;

            List<Product> list = null;
            using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Open))
            {
                list = JsonSerializer.Deserialize<List<Product>>(fs);
            }
            using (FileStream fs = new FileStream(PRODUCT_FILE_NAME, FileMode.Truncate))
            {
                list.RemoveAll(v => v.Id == dbProduct.Id);
                list.Add(dbProduct);
                JsonSerializer.Serialize(fs, list);
            }
            return dbProduct;
        }

        public static User addUser(User user)
        {
            if (!new FileInfo(FILE_NAME).Exists)
            {
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create))
                {
                    List<User> list = new List<User>();
                    list.Add(user);
                    JsonSerializer.Serialize(fs, list);
                }
            }
            else
            {
                List<User> list = null;
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
                {
                    list = JsonSerializer.Deserialize<List<User>>(fs);
                }
                User status = editUser(user);
                if (status==null)
                {
                    list.Add(user);
                    using (FileStream fs = new FileStream(FILE_NAME, FileMode.Truncate))
                    {
                        JsonSerializer.Serialize(fs, list);
                    }
                    return user;
                } else
                {
                    return status;
                }
            }
            return null;
        }

        public static User editUser(User user)
        {
            User dbUser = findByEmail(user.Email);
            if (dbUser == null)
            {
                return null;
            }


            dbUser.Photo = user.Photo;
            dbUser.Email = user.Email;
            dbUser.Password= user.Password;
            dbUser.Birthdate = user.Birthdate;
            dbUser.Name = user.Name;
            dbUser.Surname = user.Surname;
            dbUser.X = user.X;
            dbUser.Country = user.Country;
            dbUser.productIds = user.productIds;

            List<User> list = null;
            using (FileStream fs = new FileStream("user.json", FileMode.Open))
            {
                list = JsonSerializer.Deserialize<List<User>>(fs);
            }
            using (FileStream fs = new FileStream("user.json", FileMode.Truncate))
            {
                list.RemoveAll(v => v.Email== dbUser.Email);
                list.Add(dbUser);
                JsonSerializer.Serialize(fs, list);
            }
            return dbUser;
        }


         private class Index
        {
            public String key { get; 
            set;}

            public int value { get; set;}

            public Index(string key,int value)
            {
                this.key = key;
                this.value = value;
            }
        }

    }}
