using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UnityEditor
{
    [CustomGridBrush(false, true, false, "Random Brush")]
<<<<<<< HEAD
    public class RandomBrush : UnityEditor.Tilemaps.GridBrush 
=======
    public class RandomBrush : GridBrush 
>>>>>>> 5b21dec58d0929beb09376b93fb4a0af70f64aff
	{
		public TileBase[] randomTiles;
		
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
			if (randomTiles != null && randomTiles.Length > 0)
			{
				if (brushTarget == null)
					return;

				var tilemap = brushTarget.GetComponent<Tilemap>();
				if (tilemap == null)
					return;
				
				Vector3Int min = position - pivot;
				BoundsInt bounds = new BoundsInt(min, size);
				foreach (Vector3Int location in bounds.allPositionsWithin)
				{
					var randomTile = randomTiles[(int) (randomTiles.Length * UnityEngine.Random.value)];
		            tilemap.SetTile(location, randomTile);
				}
			}
			else
			{
				base.Paint(grid, brushTarget, position);
			}
        }

        [MenuItem("Assets/Create/Random Brush")]
        public static void CreateBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Random Brush", "New Random Brush", "asset", "Save Random Brush", "Assets");

            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RandomBrush>(), path);
        }
    }

    [CustomEditor(typeof(RandomBrush))]
<<<<<<< HEAD
    public class RandomBrushEditor : UnityEditor.Tilemaps.GridBrushEditor
=======
    public class RandomBrushEditor : GridBrushEditor
>>>>>>> 5b21dec58d0929beb09376b93fb4a0af70f64aff
    {
        private RandomBrush randomBrush { get { return target as RandomBrush; } }
		private GameObject lastBrushTarget;
		
		public override void PaintPreview(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (randomBrush.randomTiles != null && randomBrush.randomTiles.Length > 0)
			{
				base.PaintPreview(grid, null, position);
				
				if (brushTarget == null)
					return;

				var tilemap = brushTarget.GetComponent<Tilemap>();
				if (tilemap == null)
					return;
				
				Vector3Int min = position - randomBrush.pivot;
				BoundsInt bounds = new BoundsInt(min, randomBrush.size);
				foreach (Vector3Int location in bounds.allPositionsWithin)
				{
					var randomTile = randomBrush.randomTiles[(int) (randomBrush.randomTiles.Length * UnityEngine.Random.value)];
		            tilemap.SetEditorPreviewTile(location, randomTile);
				}
				
				lastBrushTarget = brushTarget;
			}
			else
			{
				base.PaintPreview(grid, brushTarget, position);
			}
        }
		
		public override void ClearPreview()
		{
			if (lastBrushTarget != null)
			{
				var tilemap = lastBrushTarget.GetComponent<Tilemap>();
				if (tilemap == null)
					return;
				
				tilemap.ClearAllEditorPreviewTiles();
				
				lastBrushTarget = null;
			}
			else
			{
				base.ClearPreview();
			}
		}
		
		public override void OnPaintInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
			int count = EditorGUILayout.IntField("Number of Tiles", randomBrush.randomTiles != null ? 	randomBrush.randomTiles.Length : 0);
			if (count < 0)
				count = 0;
			if (randomBrush.randomTiles == null || randomBrush.randomTiles.Length != count)
			{
				Array.Resize<TileBase>(ref randomBrush.randomTiles, count);
			}

			if (count == 0)
				return;

			EditorGUILayout.LabelField("Place random tiles.");
			EditorGUILayout.Space();

			for (int i = 0; i < count; i++)
			{
				randomBrush.randomTiles[i] = (TileBase) EditorGUILayout.ObjectField("Tile " + (i+1), randomBrush.randomTiles[i], typeof(TileBase), false, null);
			}		
			if (EditorGUI.EndChangeCheck())
				EditorUtility.SetDirty(randomBrush);
        }
    }
}
