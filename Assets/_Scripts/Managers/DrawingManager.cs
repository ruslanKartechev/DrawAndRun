using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DrawAndRun
{
    [ExecuteInEditMode]
    public class DrawingManager : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private RectTransform drawingArea;
        [SerializeField] private float lineThickness = 1f;
        [SerializeField] private float camZ = 1f;
        [SerializeField] private float threshHold = 0.1f;
        [SerializeField] private float scaleFactor = 10;
        private float zOffset = 25;
        private List<Vector3> points = new List<Vector3>();
        private List<LineRenderer> segments = new List<LineRenderer>();
        private LineRenderer line;
        private int linesCount = 0;
        private bool isDrawing = false;
        private bool takeInput = true;
        private void Awake()
        {
            if (_cam == null) _cam = FindObjectOfType<Camera>();
            GameManager.instance.eventsManager.mouseDown.AddListener(OnInputDown);
            GameManager.instance.eventsManager.mouseUp.AddListener(OnInputUp);
            GameManager.instance.eventsManager.LevelInit.AddListener(() => { takeInput = true; });
            GameManager.instance.eventsManager.LevelEnd.AddListener(() => { takeInput = false; });
        }
        private void OnInputDown()
        {
            if(takeInput == false)
            {
                return;
            }
            Vector3 mousePosition = GameManager.instance.inputManager.GetMousePosition();
            points.Clear();
            linesCount = 0;
            isDrawing = true;
            mousePosition.z = camZ;
            mousePosition = _cam.ScreenToWorldPoint(mousePosition);
            points.Add(mousePosition);

        }
        private void OnInputUp()
        {
            if (takeInput == false)
            {
                return;
            }
            isDrawing = false;
            foreach(LineRenderer rend in segments)
            {
               Destroy(rend.gameObject);
            }
              segments.Clear();

            List<Vector3> planeProjection = new List<Vector3>();
            foreach(Vector3 oldPoint in points)
            {
                Vector3 newPoint = new Vector3(oldPoint.x * scaleFactor, 0, zOffset+ oldPoint.z + oldPoint.y * Mathf.Cos(_cam.transform.eulerAngles.x)) ;
                planeProjection.Add(newPoint);
            }
            GameManager.instance.clonesSpawner.ReArrangeClones( ClonesAlignment.instance.CreateAlignment(planeProjection) );
        }
        private void Update()
        {
            if (isDrawing)
            {
                Vector3 mousePosition = GameManager.instance.inputManager.GetMousePosition();
                mousePosition.z = camZ;
                mousePosition = _cam.ScreenToWorldPoint(mousePosition);
                float distance = (mousePosition - points[points.Count - 1]).magnitude;
                if(distance > threshHold)
                {
                    points.Add(mousePosition);
                    CreateSegment(points[points.Count -2], points[points.Count-1]);
                }
            }
        }
        private void CreateSegment(Vector3 p1, Vector3 p2)
        {
            line = new GameObject("Line + " + linesCount).AddComponent<LineRenderer>();
            line.SetPosition(0, p1);
            line.SetPosition(1, p2);
            line.transform.parent = _cam.transform;
            line.material = lineMaterial;
            line.startWidth = lineThickness;
            line.endWidth = lineThickness;
            line.useWorldSpace = false;
            line.positionCount = 2;
            segments.Add(line);
        }
    }
}