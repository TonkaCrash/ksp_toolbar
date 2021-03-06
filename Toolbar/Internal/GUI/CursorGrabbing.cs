﻿/*
Copyright (c) 2013-2016, Maik Schreiber
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Cursors;

namespace Toolbar {
	internal interface ICursorGrabber {
		bool grabCursor();
	}

	internal class CursorGrabbing {
		internal static readonly CursorGrabbing Instance = new CursorGrabbing();

		private List<ICursorGrabber> grabbers = new List<ICursorGrabber>();
		private bool cursorGrabbed;

		private CursorGrabbing() {
		}

		internal void update() {
            bool grabbed = false;
            if (Application.platform == RuntimePlatform.LinuxPlayer)
               return;

			foreach (ICursorGrabber grabber in grabbers) {
				if (grabber.grabCursor()) {
					grabbed = true;
					break;
				}
			}

			if (grabbed) {
				cursorGrabbed = true;
			} else if (cursorGrabbed) {
                //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                //CursorController.Instance.ForceDefaultCursor();
                //Cursor.SetCursor(Utils.GetTexture("000_Toolbar/stockNormal", false), new Vector2(0, 0), CursorMode.ForceSoftware);

               // public CursorItem AddCursor(string id, CustomCursor defaultCursor, CustomCursor leftClickCursor = null, CustomCursor rightClickCursor = null);
                cursorGrabbed = false;
			}
		}

		internal void add(ICursorGrabber grabber) {
			grabbers.Add(grabber);
		}

		internal void remove(ICursorGrabber grabber) {
			grabbers.Remove(grabber);           
        }
	}
}
