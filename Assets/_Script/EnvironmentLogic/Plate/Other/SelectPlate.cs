using UnityEngine;

public class SelectPlate : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;

    [Header("References for checking")]
    [SerializeField] private BasePlate plateToBeSelected;

    private Renderer plateRenderer;

    private void Awake()
    {
        //set default material and renderer , here can be modified with array
        if (defaultMaterial == null)
        {
            defaultMaterial = GetComponent<Renderer>().material;
        }
        if (plateRenderer == null)
        {
            plateRenderer = GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        if(plateToBeSelected == null)
        {
            plateToBeSelected = GetComponent<BasePlate>();
        }

        Player.instance.OnSelectPlateChanged += HightlightingSelectedPlate;
    }

    private void HightlightingSelectedPlate(object sender, Player.OnSelectedPlateChangedArgs e)
    {
        if (e.newSelectedPlate == plateToBeSelected)
        {
            plateRenderer.material = highlightMaterial;
        }
        else
        {
            plateRenderer.material = defaultMaterial;
        }
    }
}
