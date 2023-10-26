using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Chiro.Ad_PictureController;
using static Chiro.Ad_ReceptionController;

namespace Chiro
{
    public class AD_Manager : MonoBehaviour
    {

        [System.Serializable]
        public class Ad
        {
            public Ad_ReceptionController receptionController;
            public List<Ad_AudioController> audioControllers;
            public Customer_PlaylistController playlistController;
            public List<Ad_PictureController> pictureControllers;
            public Ad_ProductController productController;
            public Ad_VideoController videoController;
            public Ad_SlidesController slideController;

            public bool isInitialised = false;

            
            public void InitReception(bool option)
            {
                if (receptionController != null)
                {
                    if (option)
                    {
                        receptionController.SetState(ReceptionState.IDLE);
                    }
                    else
                    {
                        receptionController.SetState(ReceptionState.NULL);
                    }
                }
                
            }

            public void InitAudio(bool option)
            {
                if (audioControllers.Count > 0)
                {
                    foreach (Ad_AudioController controller in audioControllers)
                    {
                        if (controller.tracks.Count <= 0) break;
                        if (controller != null && controller.tracks[0] != null)
                        {
                            if(option)
                            {
                                controller.SetTape(controller.tracks[0]);
                            }
                            else
                            {
                                controller.ResetTapes();
                            }
                            
                        }
                    }
                }
            }

            public void InitPicture(bool option)
            {
                if (pictureControllers.Count > 0)
                {
                    foreach (Ad_PictureController controller in pictureControllers)
                    {
                        if (controller.mapicha.Count <= 0) break;
                        if (controller != null && controller.mapicha[0] != null)
                        {
                            foreach (Picha pic in controller.mapicha.Where(pic => !pic.isInitialised))
                            {
                                if(option)
                                {
                                    pic.Init();
                                }
                                else
                                {
                                    //todo:something
                                }
                                
                            }
                        }
                    }
                }
            }

            public void InitVideo(bool option)
            {
                if (videoController != null)
                {
                    if (videoController.filmList.Count <= 0) return;
                    if (videoController != null && videoController.filmList[0] != null)
                    {
                        if (option)
                        {
                            videoController.SetFilm(videoController.filmList[0]);
                        }
                        else
                        {
                            videoController.ResetFilms();

                        }

                    }
                }
            }

            public void InitSlides(bool option)
            {
                if (slideController != null)
                {
                    if (slideController.slides.Count <= 0) return;
                    if (slideController.slides[0] != null)
                    {
                        if (option)
                        {
                            slideController.SetSlide(slideController.slides[0]);
                        }
                        else
                        {
                            slideController.ResetSlides();
                        }
                    }
                }

            }

            public void InitProduct(bool option)
            {
                if(productController != null)
                {
                    if(productController.products.Length <= 0) return;
                    if (productController.products[0] != null)
                    {
                        if (option)
                        {
                            productController.SetProduct(productController.products[0]);
                        }
                        else
                        {
                            productController.ResetProducts();
                        }
                    }
                }
            }
        }

        public Ad ad;

        public bool gamePaused = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Time.timeScale <= 0)
            {
                gamePaused = true;
            }
            else
            {
                gamePaused = false;
            }

            if (ad.isInitialised)
            {

                
            }

        }

        public void SetKioskAds()
        {
            if (ad.isInitialised) return;
            ad.InitReception(true);

            //load audio
            ad.InitAudio(true);

            //load pictures
            ad.InitPicture(true);

            //load videos
            ad.InitVideo(true);

            //load slides
            ad.InitSlides(true);

            ad.isInitialised = true;
        }

        public void UnsetKioskAds()
        {
            if (!ad.isInitialised) return;

            //unload audio
            ad.InitAudio(false);

            //unload pictures
           ad.InitPicture(false);

            //unload videos
            ad.InitVideo (false);

            //unload slides
            ad.InitSlides(false);

            //unload reception
            ad.InitReception(false);

            ad.isInitialised = false;
        }

        public void SetMarketAds()
        {
            if (ad.isInitialised) return;
            ad.InitReception(true);

            //load audio
            ad.InitAudio(true);

            //load pictures
            ad.InitPicture(true);

            //load videos
            ad.InitVideo(true);

            //load slides
            ad.InitSlides(true);

            ad.isInitialised = true;
        }

        public void UnsetMarketAds()
        {
            if (!ad.isInitialised) return;

            //unload audio
            ad.InitAudio(false);

            //unload pictures
            ad.InitPicture(false);

            //unload videos
            ad.InitVideo(false);

            //unload slides
            ad.InitSlides(false);

            //unload reception
            ad.InitReception(false);

            ad.isInitialised = false;
        }

        public Ad_VideoController GetFocusVideo()
        {
            if (ad.videoController != null)
            {
                return ad.videoController;
                
            }
            return null;
        }

        public Ad_SlidesController GetFocusDeck()
        {
            if(ad.slideController != null)
            {
                return ad.slideController;
            }
            return null;
        }

        public Ad_ProductController GetFocusProduct()
        {
            if (ad.productController != null)
            {
                return ad.productController;
            }
            return null;
        }

        public Customer_PlaylistController GetFocusRadio()
        {
            /*if(ad.audioControllers.Count > 0)
            {
                var radio = ad.audioControllers.Where(r => r.customerInAreaOfInfluence);
                return radio.FirstOrDefault();
            }*/
            if (ad.playlistController != null)
            {
                var radio = ad.playlistController;
                return radio;
            }
            return null;
        }

        public void ReduceNoise()
        {
            if (!ad.isInitialised) return;

        }
    }
}
