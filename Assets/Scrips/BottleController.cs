using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{

    public Color[] bottleColor;

    public SpriteRenderer bottleMaskRender;

    public float timeToRotate = 1.0f;

    public AnimationCurve SAMRCuve;

    public AnimationCurve FillAmountCuve;

    public float[] FillAmount;

    public float[] rotateValue;

    private int rotationIndex = 0;

    [Range(0,4)]
    public int numberOfColorInBotton = 1;

    public Color topColor;
    public int numberOfTopColorLayer = 1;

    public bool justThisBottle = false;

    public int numberOfColorTranfer;
    public BottleController bottleControlerRef;

    public Transform rightRotationPoin;
    public Transform leftRotationPoint;
    private Transform chosonRotatePoin;
    private float directionMultiptier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        bottleMaskRender.material.SetFloat("_fillMount", FillAmount[numberOfColorInBotton]);

        updateColorBottle();

        updateTopColorValue();
    }

    

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyUp(KeyCode.P) && justThisBottle == true)
        //{
         
           

        //    //if (bottleControlerRef.FillBottleCheck(topColor))  // do vao ref
             
        //    //{ 
        //    //    chosonRotatePointDerection();

        //    //    numberOfColorTranfer = Mathf.Min(numberOfTopColorLayer, 4 - bottleControlerRef.numberOfColorInBotton);

        //    //    for (int i = 0; i < numberOfColorTranfer; i++)
        //    //    {
        //    //        //   bottleControlerRef.bottleColor[3 - bottleControlerRef.numberOfColorInBotton + i] = topColor;
        //    //        bottleControlerRef.bottleColor[bottleControlerRef.numberOfColorInBotton+i] = topColor;
        //    //        // bottleControlerRef.bottleColor[3] = topColor;                   

        //    //    }
        //    //    bottleControlerRef.updateColorBottle();


        //    //}

        //    //calculateRotationIndex(4 - bottleControlerRef.numberOfColorInBotton);

        //    //StartCoroutine(rotateBottle());
        //}
        
    }


    public void startColorTransfer()
    {

        chosonRotatePointDerection();

        numberOfColorTranfer = Mathf.Min(numberOfTopColorLayer, 4 - bottleControlerRef.numberOfColorInBotton);

        for (int i = 0; i < numberOfColorTranfer; i++)
        {
            //   bottleControlerRef.bottleColor[3 - bottleControlerRef.numberOfColorInBotton + i] = topColor;
            bottleControlerRef.bottleColor[bottleControlerRef.numberOfColorInBotton + i] = topColor;
            // bottleControlerRef.bottleColor[3] = topColor;                   

        }
        bottleControlerRef.updateColorBottle();


        calculateRotationIndex(4 - bottleControlerRef.numberOfColorInBotton);

        StartCoroutine(rotateBottle());
    }

    void updateColorBottle()
    {
     //   bottleMaskRender.material.SetFloat("_SARM", 0.38f);
        bottleMaskRender.material.SetColor("_C1", bottleColor[0]);
        bottleMaskRender.material.SetColor("_C2", bottleColor[1]);
        bottleMaskRender.material.SetColor("_C3", bottleColor[2]);
        bottleMaskRender.material.SetColor("_C4", bottleColor[3]);
        bottleMaskRender.material.SetFloat("_fillMount", FillAmount[numberOfColorInBotton]);
    }

    IEnumerator rotateBottle()
    {
        float t = 0;
        float lerpValue;
        float angleValue;
        float LastAngleValue = 0;


        float angleValueRotate;
        float lastAngleValueRotate =0;

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;

            angleValue = Mathf.Lerp(0, rotateValue[rotationIndex], lerpValue);

            angleValueRotate = Mathf.Lerp(0, directionMultiptier * rotateValue[rotationIndex], lerpValue);
            // transform.eulerAngles = new Vector3 (0, 0, angleValue);

            transform.RotateAround(chosonRotatePoin.position, Vector3.forward, lastAngleValueRotate - angleValueRotate);


            bottleMaskRender.material.SetFloat("_SARM", SAMRCuve.Evaluate(angleValue));

            if (angleValue < rotateValue[rotationIndex])
            {


                if(angleValue > rotateValue[numberOfColorInBotton])
                {

                    bottleMaskRender.material.SetFloat("_fillMount", FillAmountCuve.Evaluate(angleValue));

                    bottleControlerRef.fillUp(FillAmountCuve.Evaluate(LastAngleValue) - FillAmountCuve.Evaluate(angleValue));
                  

                }

                lastAngleValueRotate = angleValueRotate;
                LastAngleValue = angleValue;
            }
            else
            {
                break;
            }
               
            t += Time.deltaTime ;

            yield return new WaitForEndOfFrame();
        }

        //angleValue = 90f;

        //transform.eulerAngles = new Vector3(0, 0, angleValue);

        //bottleMaskRender.material.SetFloat("_SARM", FillAmount[numberOfColorInBotton]);
        //bottleMaskRender.material.SetFloat("_fillMount", FillAmount[numberOfColorInBotton]);

        //   numberOfColorInBotton -= numberOfTopColorLayer;

        numberOfColorInBotton -= numberOfColorTranfer;

        bottleControlerRef.numberOfColorInBotton += numberOfColorTranfer;

        bottleControlerRef.bottleMaskRender.material.SetFloat("_fillMount", FillAmount[bottleControlerRef.numberOfColorInBotton]);

        StartCoroutine(rotateBottleCallback());

    }

    IEnumerator rotateBottleCallback()
    {

        float t = 0;
        float lerpValue;
        float angleValue;      
        float lastAngleValue = rotateValue[rotationIndex];

        float angleValueRotate;
        float lastAngleValueRotate = directionMultiptier * rotateValue[rotationIndex];

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;

             angleValue = Mathf.Lerp( rotateValue[rotationIndex], 0, lerpValue);
             angleValueRotate = Mathf.Lerp(directionMultiptier * rotateValue[rotationIndex], 0, lerpValue);

            // transform.RotateAround( chosonRotatePoin.position, Vector3.forward, lastAngleValue - angleValue);

            transform.RotateAround(chosonRotatePoin.position, Vector3.forward, lastAngleValueRotate-angleValueRotate);

            //transform.eulerAngles = new Vector3(0, 0, angleValue);

            if (angleValue<rotateValue[rotationIndex])
            {
                  bottleMaskRender.material.SetFloat("_SARM", SAMRCuve.Evaluate(angleValue));

            }


            t += Time.deltaTime ;

            lastAngleValueRotate = angleValueRotate;

            lastAngleValue = angleValue;

            yield return new WaitForEndOfFrame();
        } 

        updateTopColorValue();

        bottleControlerRef.updateTopColorValue();

        bottleMaskRender.material.SetFloat("_fillMount", FillAmount[numberOfColorInBotton]);
        angleValue = 0f;

      //  transform.eulerAngles = new Vector3(0, 0, angleValue);

        bottleMaskRender.material.SetFloat("_SARM", SAMRCuve.Evaluate(angleValue));
        

    }

   public  void updateTopColorValue()
    {
        if (numberOfColorInBotton != 0)
        {
            numberOfTopColorLayer = 1;

            topColor = bottleColor[numberOfColorInBotton - 1];

            if(numberOfColorInBotton == 4)
            {
                if (bottleColor[3].Equals(bottleColor[2]))
                {
                    numberOfTopColorLayer = 2;

                    if (bottleColor[2].Equals(bottleColor[1]))
                        {
                            numberOfTopColorLayer = 3;

                        if (bottleColor[1].Equals(bottleColor[0]))
                        {
                            numberOfTopColorLayer = 4;                    
                        }
                    }

                }
               

            }
            else if (numberOfColorInBotton == 3)
            {
                if (bottleColor[2].Equals(bottleColor[1]))
                {
                    numberOfTopColorLayer = 2;

                    if (bottleColor[1].Equals(bottleColor[0]))
                    {
                        numberOfTopColorLayer = 3;

                       
                    }

                }


            }else if(numberOfColorInBotton == 2)
            {
                if (bottleColor[1].Equals(bottleColor[0]))
                {
                    numberOfTopColorLayer = 2;
                   

                }
            }


        }

        rotationIndex = numberOfColorInBotton - numberOfTopColorLayer;

    }
    
    public bool FillBottleCheck(Color colorToCheck)
    {
        if (numberOfColorInBotton == 0)
            return true;
        else
        {
            if (numberOfColorInBotton == 4)
            {
                return false;
            }
            else
            {
                if (topColor.Equals(colorToCheck))
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

    private void calculateRotationIndex(int numberOfEmptySpacesInSecondBottle)
    {
        rotationIndex =  (numberOfColorInBotton - Mathf.Min(numberOfEmptySpacesInSecondBottle, numberOfTopColorLayer));

    }

    private void fillUp(float fillAmountAdd)
    {
        bottleMaskRender.material.SetFloat("_fillMount", bottleMaskRender.material.GetFloat("_fillMount") + fillAmountAdd);
    }

    private void chosonRotatePointDerection()
    {
        if(transform.position.x > bottleControlerRef.transform.position.x)
        {
            chosonRotatePoin = leftRotationPoint;
            directionMultiptier = -1.0f;
        }
        else
        {
            chosonRotatePoin = rightRotationPoin;
            directionMultiptier = 1.0f;

        }


    }

}
