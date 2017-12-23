﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIMission : MonoBehaviour
{
    public const string DEFAULT_GUITEXT_NUMBER_FORMAT = "#{0}";
    public const char DEFAULT_GUITEXT_DIFFICULTY_CHAR = '✦';
    public const string DEFAULT_GUICURSOR_CAPTION_FORMAT = "//{0}";

    public RectTransform rectTransform;
    public Text guiTextTitle;
    public Text guiTextID;
    public Text guiTextDescription;
    public Text guiTextDifficulty;
    public GUICursor guiCursor;

    public PlanetHub planet;
    public int selectedMissionIndex;
    public Mission selectedMission;

    private bool isShifting = false;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
	
	public void Update()
    {
        if(rectTransform != null)
        {
            if(selectedMission != null)
            {
                int shift = Mathf.RoundToInt(Input.GetAxis("FormNavigate"));
                if (shift != 0)
                {
                    if (!isShifting)
                    {
                        isShifting = true;
                        MissionChange(selectedMissionIndex + shift);
                    }
                }
                else
                {
                    isShifting = false;
                }
            }
            else
            {
                MissionChange(0);
            }

            MissionUpdate();
        }
	}

    public void MissionChange(int index)
    {
        if(planet != null)
        {
            Mission[] missions = planet.GetComponentsInChildren<Mission>();
            int indexRelative = (index + missions.Length) % missions.Length;
            if (missions.Length > 0)
            {
                selectedMissionIndex = indexRelative;
                selectedMission = missions[indexRelative];
            }
        }
    }

    public void MissionUpdate()
    {
        if (selectedMission != null)
        {
            if (guiTextTitle != null)
            {
                guiTextTitle.text = selectedMission.locationName;
            }

            if (guiTextID != null)
            {
                guiTextID.text = string.Format(DEFAULT_GUITEXT_NUMBER_FORMAT, selectedMission.id.ToString("000"));
            }

            if (guiTextDescription != null)
            {
                guiTextDescription.text = selectedMission.description;
            }

            if (guiTextDifficulty != null)
            {
                guiTextDifficulty.text = new String(DEFAULT_GUITEXT_DIFFICULTY_CHAR, (int)selectedMission.difficulty);
            }

            if (guiCursor != null)
            {
                guiCursor.target = selectedMission.transform;
                guiCursor.guiTextCaption.text = string.Format(DEFAULT_GUICURSOR_CAPTION_FORMAT, selectedMission.locationName.ToUpper().Replace(" ", "_"));
            }
        }
    }
}
