<!-- default file list -->
*Files to look at*:

* [MyWizardControl.cs](./CS/Q208175/MyWizardControl.cs) (VB: [MyWizardControl.vb](./VB/Q208175/MyWizardControl.vb))
<!-- default file list end -->
# How to change height of the WizardPage header based on the DescriptionText


<p>To accomplish this task, create a Wizard97Model descendant and override its GetInteriorHeaderBounds and GetInteriorPageBounds methods to change the header height. This example demonstrates how to automatically calculate height based on the description text.</p>

<br/>


