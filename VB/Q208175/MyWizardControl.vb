Imports Microsoft.VisualBasic
Imports DevExpress.XtraWizard
Imports System.Drawing
Imports System

Namespace DXSample
	Public Class MyWizardControl
		Inherits WizardControl
		Public Sub New()
			MyBase.New()
		End Sub
		Protected Overrides Function CreateViewInfo() As WizardViewInfo
			Return New MyWizardViewInfo(Me)
		End Function
	End Class
	Public Class MyWizardViewInfo
		Inherits WizardViewInfo
		Public Sub New(ByVal control As WizardControl)
			MyBase.New(control)
		End Sub
		Protected Overrides Function CreateWizardModelCore(ByVal style As WizardStyle) As WizardModelBase
			If style = WizardStyle.Wizard97 Then
				Return New MyWizard97Model(Me)
			End If
			Return MyBase.CreateWizardModelCore(style)
		End Function
		Friend ReadOnly Property AppearancesInternal() As WizardAppearances
			Get
				Return Appearances
			End Get
		End Property
		Friend Function GetDividerSizeInternal() As Integer
			Return GetDividerSize()
		End Function
	End Class
	Public Class MyWizard97Model
		Inherits WizardViewInfo.Wizard97Model
		Public Sub New(ByVal viewInfo As WizardViewInfo)
			MyBase.New(viewInfo)
		End Sub
		Private interiorHeaderBounds As Rectangle
		Public Overrides Function GetInteriorHeaderBounds() As Rectangle
			Return GetInteriorHeaderBounds(True, Nothing)
		End Function
		Public Overrides Function GetInteriorPageBounds(ByVal page As BaseWizardPage) As Rectangle
			Dim result As Rectangle = GetContentBounds()
			Dim h As Integer = GetInteriorHeaderBounds(False, page).Height + (CType(ViewInfo, MyWizardViewInfo)).GetDividerSizeInternal()
			result.Y += h
			result.Height -= h
			If Wizard.AllowPagePadding Then
				result.Inflate(-Wizard97Consts.ContentMargin, -Wizard97Consts.ContentMargin)
			End If
			Return result
		End Function
        Private Overloads Function GetInteriorHeaderBounds(ByVal useCached As Boolean, ByVal page As BaseWizardPage) As Rectangle
            If (Not useCached) OrElse interiorHeaderBounds = Nothing Then
                interiorHeaderBounds = GetInteriorHeaderBoundsCore(page)
            End If
            Return interiorHeaderBounds
        End Function
		Private Function GetInteriorHeaderBoundsCore(ByVal page As BaseWizardPage) As Rectangle
			Dim result As Rectangle = GetContentBounds()
			If page Is Nothing Then
				page = CType(Wizard.SelectedPage, WizardPage)
			End If
			If Not(TypeOf page Is WizardPage) Then
				Return MyBase.GetInteriorHeaderBounds()
			End If
			Dim appearances As WizardAppearances = (CType(ViewInfo, MyWizardViewInfo)).AppearancesInternal
			result.Height = CalcTextSize(page.Text, appearances.Header, result.Width).Height
			Dim descriptionWidth As Integer = result.Width - (8 * Wizard97Consts.ContentMargin)
			result.Height += CalcTextSize((CType(page, WizardPage)).DescriptionText, appearances.Page, descriptionWidth).Height
			result.Height += 2 * Wizard97Consts.ContentMargin
			Return result
		End Function
	End Class
End Namespace