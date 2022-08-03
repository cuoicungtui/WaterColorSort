using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public BottleController FirtBottle;

    public BottleController SecondBottle;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
           // Debug.Log("kich chot");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

           // Debug.Log(hit.collider.GetComponent<BottleController>());

            if (hit.collider != null)
            {


                if (hit.collider.GetComponent<BottleController>() != null)
                {
                    if (FirtBottle == null)
                    {
                        FirtBottle = hit.collider.GetComponent<BottleController>();
                    }
                    else
                    {
                        if (FirtBottle == hit.collider.GetComponent<BottleController>())
                        {
                            FirtBottle = null;
                        }
                        else
                        {
                            SecondBottle = hit.collider.GetComponent<BottleController>();
                            FirtBottle.bottleControlerRef = SecondBottle;

                            FirtBottle.updateTopColorValue();
                            SecondBottle.updateTopColorValue();
                            if (SecondBottle.FillBottleCheck(FirtBottle.topColor))
                            {
                                FirtBottle.startColorTransfer();


                                FirtBottle = null;
                                SecondBottle = null;

                            }
                            else
                            {
                                FirtBottle = null;
                                SecondBottle = null;
                            }

                        }

                    }

                }
            }
           


        }

    }
}