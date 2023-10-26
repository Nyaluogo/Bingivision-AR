using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Chiro
{
    public class Ad_ProductController : MonoBehaviour
    {
        [System.Serializable]
        public class Product
        {
            public string name;
            public string description;
            public GameObject productObj;
            public bool isShowing = false;
        }
        public Product[] products;
        public Product currentProduct;

        public Transform productRoot;
        public float rotateSpeed = 30f;
        public Vector3 defaultRotation = Vector3.zero;


        // Start is called before the first frame update
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            SetUpdateReferences();
        }



        void SetInitialReferences()
        {
            if(products.Length > 0)
            {
                currentProduct = products[0];
            }
        }

        void SetUpdateReferences()
        {
            
        }

        public void SetProduct(Product prod)
        {
            
            if(prod.productObj != null)
            {
                if(currentProduct == null || currentProduct != prod)
                {
                    ResetProducts();
                    prod.productObj.SetActive(true);
                    prod.isShowing = true;
                    currentProduct = prod;
                }
                
            }
            
        }

        public void ResetProducts()
        {
            if(products.Length > 0)
            {
                if (productRoot != null)
                {
                    //productRoot.Rotate(defaultRotation);
                    //productRoot.eulerAngles = defaultRotation;
                    productRoot.localRotation = Quaternion.EulerAngles(0, 0, 0);
                    productRoot.localScale = Vector3.one;
                }

                foreach (Product p in products)
                {
                    if(p.productObj != null)
                    {
                        p.productObj.SetActive(false);
                        p.isShowing = false;
                    }
                }
                currentProduct = null;
            }
        }

        public void RotateUp()
        {
            if(currentProduct.productObj != null)
            {
                var prod = currentProduct.productObj;

                if(productRoot != null)
                {
                    productRoot.Rotate(Vector3.left * -rotateSpeed * Time.deltaTime);
                }
            }
        }

        public void RotateDown()
        {
            if (currentProduct.productObj != null)
            {
                var prod = currentProduct.productObj;

                if (productRoot != null)
                {
                    productRoot.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
                }
            }
        }

        public void RotateRight()
        {
            if (currentProduct.productObj != null)
            {
                var prod = currentProduct.productObj;

                if (productRoot != null)
                {
                    productRoot.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
                }
            }
        }

        public void RotateLeft()
        {
            if (currentProduct.productObj != null)
            {
                var prod = currentProduct.productObj;

                if (productRoot != null)
                {
                    productRoot.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                }
            }
        }

        public void SetScale(float factor)
        {
            if (currentProduct.productObj != null)
            {
                
                var prod = currentProduct.productObj;
                Vector3 target_scale = Vector3.one;
                if (productRoot != null)
                {
                    target_scale = new Vector3(target_scale.x * factor, target_scale.y * factor, target_scale.z * factor);
                    productRoot.localScale = target_scale;
                }
            }
        }
    }
}
