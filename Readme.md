<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Q208175/Form1.cs) (VB: [Form1.vb](./VB/Q208175/Form1.vb))
* [MyWizardControl.cs](./CS/Q208175/MyWizardControl.cs) (VB: [MyWizardControl.vb](./VB/Q208175/MyWizardControl.vb))
* [Program.cs](./CS/Q208175/Program.cs) (VB: [Program.vb](./VB/Q208175/Program.vb))
<!-- default file list end -->
# How to change height of the WizardPage header based on the DescriptionText


<p>To accomplish this task, create a Wizard97Model descendant and override its GetInteriorHeaderBounds and GetInteriorPageBounds methods to change the header height. This example demonstrates how to automatically calculate height based on the description text.</p>

<br/>


