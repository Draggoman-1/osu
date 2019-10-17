﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Catch.Objects.Drawable
{
    public class DrawableBananaShower : DrawableCatchHitObject<BananaShower>
    {
        private readonly Func<CatchHitObject, DrawableHitObject<CatchHitObject>> createDrawableRepresentation;
        private readonly Container bananaContainer;

        public DrawableBananaShower(BananaShower s, Func<CatchHitObject, DrawableHitObject<CatchHitObject>> createDrawableRepresentation = null)
            : base(s)
        {
            this.createDrawableRepresentation = createDrawableRepresentation;
            RelativeSizeAxes = Axes.X;
            Origin = Anchor.BottomLeft;
            X = 0;

            AddInternal(bananaContainer = new Container { RelativeSizeAxes = Axes.Both });
        }

        protected override void AddNested(DrawableHitObject h)
        {
            base.AddNested(h);
            bananaContainer.Add(h);
        }

        protected override void ClearNested()
        {
            base.ClearNested();
            bananaContainer.Clear();
        }

        protected override DrawableHitObject CreateNested(HitObject hitObject)
        {
            switch (hitObject)
            {
                case Banana banana:
                    return createDrawableRepresentation?.Invoke(banana)?.With(o => ((DrawableCatchHitObject)o).CheckPosition = p => CheckPosition?.Invoke(p) ?? false);
            }

            return base.CreateNested(hitObject);
        }
    }
}
