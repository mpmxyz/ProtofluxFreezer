using Elements.Core;
using FrooxEngine;
using System.Collections.Generic;
using UIXDialogBuilder;

namespace ProtofluxFreezerRML.Mod.Common
{
    internal class FreezeDialogState : IDialogState
    {
        [DialogOption("Parent of nodes")]
        private Slot nodeRoot;

        [DialogOption("Edit mode driver")]
        private ValueMultiDriver<bool> editModeDriver;

        [DialogOption("Child index driver")]
        private ValueMultiDriver<int> childIndexDriver;

        [DialogAction("Freeze nodes", isPrivate: false)]
        private void Execute()
        {
            foreach(var child in nodeRoot.Children)
            {
                if (!child.ParentReference.IsLinked)
                {
                    editModeDriver.Drives.Add().Target = ForceValue(child.ParentReference);
                }
                if (!child.Position_Field.IsLinked)
                {
                    editModeDriver.Drives.Add().Target = ForceValue(child.Position_Field);
                }
                if (!child.Rotation_Field.IsLinked)
                {
                    editModeDriver.Drives.Add().Target = ForceValue(child.Rotation_Field);
                }
                if (!child.Scale_Field.IsLinked)
                {
                    editModeDriver.Drives.Add().Target = ForceValue(child.Scale_Field);
                }
                var switcher = child.GetComponentOrAttach<BooleanSwitcher>();
                if (!switcher.ActiveIndex.IsLinked)
                {
                    childIndexDriver.Drives.Add().Target = switcher.ActiveIndex;
                }
                switcher.AutoAddChildren.Value = true;
            }
        }

        private static Sync<bool> ForceValue<T>(IField<T> prop) where T : unmanaged
        {
            var field = prop.FindNearestParent<Slot>().AttachComponent<ValueField<T>>();
            var vCopy = prop.DriveFrom(field.Value, keepOriginalValue: true);
            return vCopy.WriteBack;

        }

        private static Sync<bool> ForceValue<T>(SyncRef<T> prop) where T : class, IWorldElement
        {
            var field = prop.FindNearestParent<Slot>().AttachComponent<ReferenceField<T>>();
            var vCopy = prop.DriveFrom(field.Reference, keepOriginalValue: true);
            return vCopy.WriteBack;

        }

        public Dialog Dialog { get; set; }

        public void Dispose()
        {
            
        }

        public IDictionary<object, string> UpdateAndValidate(object key)
        {
            var errors = new Dictionary<object, string>();
            if (nodeRoot == null || nodeRoot.IsDestroyed)
            {
                errors[nameof(nodeRoot)] = "Slot is required";
            }
            if (editModeDriver == null || editModeDriver.IsDestroyed)
            {
                errors[nameof(editModeDriver)] = "ValueMultiDriver<bool> is required";
            }
            if (childIndexDriver == null || childIndexDriver.IsDestroyed)
            {
                errors[nameof(childIndexDriver)] = "ValueMultiDriver<int> is required";
            }
            return errors;
        }
    }
}
