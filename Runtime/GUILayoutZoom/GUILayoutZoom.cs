﻿using UnityEngine;

namespace MA_Toolbox.MA_Editor
{
	public class GUILayoutZoom
	{
		private const float EditorWindowTabHeight = 21.0f; //The height of the editor window top bar. (were the name, zoom and exit buttons are)
		private static Matrix4x4 prevGuiMatrix;

		public static Rect BeginArea(float zoomScale, Rect screenCoordsArea)
		{
			GUI.EndGroup();        // End the group Unity begins automatically for an EditorWindow to clip out the window tab. This allows us to draw outside of the size of the EditorWindow.

			Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.TopLeft());
			clippedArea.y += EditorWindowTabHeight;
			GUI.BeginGroup(clippedArea);

			prevGuiMatrix = GUI.matrix;
			Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
			Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
			GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

			return clippedArea;
		}

		public static void EndArea()
		{
			GUI.matrix = prevGuiMatrix;
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(0.0f, EditorWindowTabHeight, Screen.width, Screen.height));
		}
	}
}