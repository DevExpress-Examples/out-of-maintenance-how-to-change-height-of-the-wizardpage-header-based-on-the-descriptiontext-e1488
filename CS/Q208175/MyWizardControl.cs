using DevExpress.XtraWizard;
using System.Drawing;
using System;

namespace DXSample {
    public class MyWizardControl : WizardControl {
        public MyWizardControl() : base() { }
        protected override WizardViewInfo CreateViewInfo() {
            return new MyWizardViewInfo(this);
        }
    }
    public class MyWizardViewInfo : WizardViewInfo {
        public MyWizardViewInfo(WizardControl control) : base(control) { }
        protected override WizardModelBase CreateWizardModelCore(WizardStyle style) {
            if (style == WizardStyle.Wizard97)
                return new MyWizard97Model(this);
            return base.CreateWizardModelCore(style);
        }
        internal WizardAppearanceCollection AppearancesInternal { get { return PaintAppearance; } }
        internal int GetDividerSizeInternal() { return GetDividerSize(); }
    }
    public class MyWizard97Model : WizardViewInfo.Wizard97Model {
        public MyWizard97Model(WizardViewInfo viewInfo) : base(viewInfo) { }
        private Rectangle interiorHeaderBounds;
        public override Rectangle GetInteriorHeaderBounds() {
            return GetInteriorHeaderBounds(true, null);
        }
        public override Rectangle GetInteriorPageBounds(BaseWizardPage page) {
            Rectangle result = GetContentBounds();
            int h = GetInteriorHeaderBounds(false, page).Height + ((MyWizardViewInfo)ViewInfo).GetDividerSizeInternal();
            result.Y += h;
            result.Height -= h;
            if (Wizard.AllowPagePadding)
                result.Inflate(-Wizard97Consts.ContentMargin, -Wizard97Consts.ContentMargin);
            return result;
        }
        private Rectangle GetInteriorHeaderBounds(bool useCached, BaseWizardPage page) {
            if (!useCached || interiorHeaderBounds == null)
                interiorHeaderBounds = GetInteriorHeaderBoundsCore(page);
            return interiorHeaderBounds;
        }
        private Rectangle GetInteriorHeaderBoundsCore(BaseWizardPage page) {
            Rectangle result = GetContentBounds();
            if (page == null)
                page = (WizardPage)Wizard.SelectedPage;
            if (!(page is WizardPage))
                return base.GetInteriorHeaderBounds();
            WizardAppearanceCollection appearances = ((MyWizardViewInfo)ViewInfo).AppearancesInternal;
            result.Height = CalcTextSize(page.Text, appearances.PageTitle, result.Width).Height;
            int descriptionWidth = result.Width - (8 * Wizard97Consts.ContentMargin);
            result.Height += CalcTextSize(((WizardPage)page).DescriptionText, appearances.Page, descriptionWidth).Height;
            result.Height += 2 * Wizard97Consts.ContentMargin;
            return result;
        }
    }
}