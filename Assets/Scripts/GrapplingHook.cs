﻿﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class GrapplingHook : MonoBehaviour
    {
        public float Length = 4;
        public float SwingSpeed = 5;
        public int RetractSpeed = 10;

        [HideInInspector]
        public bool IsEnabled
        {
            get { return _points.Any(); }
        }

        private readonly List<GameObject> _points = new List<GameObject>();
        private LineRenderer _line;
        private GameObject _grapple;
        private GameObject _previousGrapple;
        private float _previousDistance = -1;
        private DistanceJoint2D _joint;

        void Start()
        {
            _line = new GameObject("Line").AddComponent<LineRenderer>();
            _line.SetVertexCount(2);
            _line.SetWidth(.025f, .025f);
            _line.gameObject.SetActive(false);
            _line.SetColors(Color.red, Color.red);
            _line.renderer.material.color = Color.red;
            
            _grapple = new GameObject("Grapple");
            _grapple.AddComponent<CircleCollider2D>().radius = .1f;
            _grapple.AddComponent<Rigidbody2D>();
            _grapple.rigidbody2D.isKinematic = true;

            _previousGrapple = (GameObject)Instantiate(_grapple);
            _previousGrapple.name = "Previous Grapple";

            _joint = gameObject.AddComponent<DistanceJoint2D>();
            _joint.enabled = false;
        }

        void Update()
        {
            if (IsEnabled) UpdateGrapple();
            else CheckForGrapple();
        }

        private void CheckForGrapple()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                var grapplePoint = transform.position + (worldPosition - transform.position) * Length;

                var hit = Physics2D.Linecast(transform.position, grapplePoint);
                var distance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider != null && distance < Length && hit.collider.gameObject.layer == 8)
                {
                    _line.SetVertexCount(2);
                    _line.SetPosition(0, hit.point);
                    _line.SetPosition(1, transform.position);
                    _line.gameObject.SetActive(true);

                    _points.Add(CreateGrapplePoint(hit));

                    _grapple.transform.position = hit.point;
                    SetParent(_grapple.transform, hit.collider.transform);

                    _joint.enabled = true;
                    _joint.connectedBody = _grapple.GetComponent<Rigidbody2D>();
                    _joint.distance = Vector3.Distance(hit.point, transform.position);
                    _joint.maxDistanceOnly = true;
                }
            }
        }

        private GameObject CreateGrapplePoint(RaycastHit2D hit)
        {
            var p = new GameObject("GrapplePoint");
            SetParent(p.transform, hit.collider.transform);
            p.transform.position = hit.point;
            return p;
        }

        private void UpdateGrapple()
        {
            UpdateLineDrawing();

            var hit = Physics2D.Linecast(transform.position, _grapple.transform.position);
            var hitPrev = Physics2D.Linecast(transform.position, _previousGrapple.transform.position);

            if (hit.collider.gameObject != _grapple && hit.collider.gameObject != _previousGrapple)
            {
                if (hit.collider.gameObject.name == "CutCube")
                {
                    RetractRope();
                }
                // if you lose line of sight on the grappling hook, then add a new point to wrap around

                _points.Add(CreateGrapplePoint(hit));

                UpdateLineDrawing();

                _previousGrapple.transform.position = _grapple.transform.position;
                SetParent(_previousGrapple.transform, _grapple.transform.parent);
                _grapple.transform.position = hit.point;
                SetParent(_grapple.transform, hit.collider.transform);
                _previousDistance = -1;

                _joint.distance -= Vector3.Distance(_grapple.transform.position, _previousGrapple.transform.position);
            }
            else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                // if you retract the grappling hook

                // jump off
                if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < _grapple.transform.position.y)
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 3);

                RetractRope();
            }
            else if (Vector3.Distance(_grapple.transform.position, _previousGrapple.transform.position) <= .1f)
            {
                RemoveLastCollider();
            }
            else
            {
                // always update the last points in the line to track player

                _line.SetPosition(_points.Count, transform.position);
                rigidbody2D.AddForce(Vector3.right * Input.GetAxisRaw("Horizontal") * SwingSpeed);

                // Inefficient... TO THE MAX!
                float totalLength = _joint.distance;
                for (int i = 0; i < _points.Count - 1; i++)
                {
                    totalLength += (_points[i].transform.position - _points[i + 1].transform.position).magnitude;
                }

                _joint.distance -= _joint.distance - (transform.position - _grapple.transform.position).magnitude;

                var retractDelta = Input.GetAxisRaw("Vertical") * RetractSpeed * Time.deltaTime;
                if (totalLength - retractDelta < Length)
                {
                    _joint.distance -= retractDelta;
                }

                // if you can see previous point then unroll back to that point
                if (hitPrev.collider != null && hitPrev.transform == _previousGrapple.transform)
                    RemoveLastCollider();
            }

            UpdateDistance();
        }

        private void RetractRope()
        {
            _joint.enabled = false;
            _line.gameObject.SetActive(false);
            _points.ForEach(Destroy);
            _points.Clear();
            _grapple.transform.position = new Vector3(0, 0, -1);
            _previousGrapple.transform.position = new Vector3(0, 0, -1);
            _previousDistance = -1;
        }

        private void RemoveLastCollider()
        {
            if (_points.Count > 1)
            {
                Destroy(_points[_points.Count - 1]);
                _points.RemoveAt(_points.Count - 1);

                UpdateLineDrawing();

                _joint.distance += Vector3.Distance(_grapple.transform.position, _previousGrapple.transform.position);
                _grapple.transform.position = _previousGrapple.transform.position;
                SetParent(_grapple.transform, _previousGrapple.transform.parent);
            }

            if (_points.Count > 1)
                _previousGrapple.transform.position = _points.ElementAt(_points.Count - 2).transform.position;
            else
                _previousGrapple.transform.position = new Vector3(0, 0, -1);

            _previousDistance = -1;
        }

        private void UpdateLineDrawing()
        {
            _line.SetVertexCount(_points.Count + 1);
            for (var i = 0; i < _points.Count; i++)
                _line.SetPosition(i, _points[i].transform.position);
            _line.SetPosition(_points.Count, transform.position);
        }

        private void UpdateDistance()
        {
            if (_points.Count == 0) return;

            var distance = 0f;

            for (var i = 1; i < _points.Count; i++)
                distance += Vector3.Distance(_points[i - 1].transform.position, _points[i].transform.position);
            distance += Vector3.Distance(_points[_points.Count - 1].transform.position, transform.position);

            if (_previousDistance > 0)
                _joint.distance += _previousDistance - distance;

            _previousDistance = distance;
        }

        private void SetParent(Transform child, Transform parent)
        {
            child.SetParent(parent);
            if (parent != null)
                child.localScale = new Vector3(1 / parent.localScale.x, 1 / parent.localScale.y, 1 / parent.localScale.z);
        }
    }
}