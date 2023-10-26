using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Chiro
{
    public class Ad_SlidesController : MonoBehaviour
    {
        [System.Serializable]
        public class Slide
        {
            public string name;
            public string description;
            public Texture picTexture;
            public bool isShowing;
            
        }
        public List<Slide> slides;

        public Slide currentSlide;

        public RawImage screenRawImage;
        public UI_Billboard billboard;

        public Texture defaultTexture;
        public float delay = 2f;
        
        public bool isPresenting = false;
        public bool isFullScreen = false;
        public bool isPlaying = false;

        public bool customerInAreaOfInfluence = false;
        public bool initOnStart = false;

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
            PauseSlideshow();
        }

        void SetUpdateReferences()
        {
            if (!isPlaying)
            {
                StartCoroutine(SlidePresentation());
            }

        }

        public void SetSlide(Slide slide)
        {
            if(screenRawImage != null)
            {
                if(slide.picTexture != null)
                {
                    if(slide.picTexture == screenRawImage.texture) { return; }

                    screenRawImage.texture = slide.picTexture;
                    
                    ResetSlides();

                    slide.isShowing = true;

                    currentSlide = slide;
                }
            }
        }

        public void ResetSlides()
        {
            currentSlide = null;

            foreach (Slide sld in slides.Where(s => s.isShowing))
            {
                sld.isShowing = false;
            }
        }

        public void TogglePresentation(bool option)
        {
            if(option)
            {
                if(!isPlaying)
                {
                    StartCoroutine(SlidePresentation());
                }
                isPresenting = true;
            }
            else
            {
                StopAllCoroutines();
                isPresenting = false;
                isPlaying = false;
            }

        }

        public IEnumerator SlidePresentation()
        {

            if ( isPresenting)
            {
                isPlaying = true;
                yield return new WaitForSeconds(delay);
                NextSlide();
                isPlaying = false;

            }

        }

        public void PlaySlideshow()
        {
            TogglePresentation(true);
        }

        public void PauseSlideshow()
        {
            TogglePresentation(false);
        }

        public void NextSlide()
        {
            if(screenRawImage != null)
            {
                if(slides.Count > 0)
                {
                    int current_index = slides.IndexOf(currentSlide);

                    if(current_index != slides.Count - 1)
                    {
                        SetSlide(slides[current_index + 1]);
                    }
                    else
                    {
                        SetSlide(slides[0]);
                    }
                }
            }

        }

        public void PreviousSlide()
        {
            if (screenRawImage != null)
            {
                if (slides.Count > 0)
                {
                    int current_index = slides.IndexOf(currentSlide);

                    if (current_index != 0)
                    {
                        SetSlide(slides[current_index - 1]);
                    }
                    else
                    {
                        SetSlide(slides[slides.Count - 1]);
                    }
                }

            }
        }

        public void ToggleFullscreen()
        {

        }
    }
}
