using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using EAuctionProj.Utility;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class EditUserRegister : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(EditUserRegister));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Session.Clear();
                Session.Abandon();
                ViewState.Clear();
                FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                Session["UpdResult"] = null;

                string _companyNo = Request.QueryString["CompanyNo"];

                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                hdfUserName.Value = retUser.UserName.ToString().Trim();
                hdfUsersNo.Value = retUser.UsersNo.ToString().Trim();
                hdfRoleNo.Value = retUser.RolesNo.ToString().Trim();

                hdfCompanyNo.Value = string.IsNullOrWhiteSpace(_companyNo) ? retUser.CompanyNo.ToString().Trim() : _companyNo;

                GetCompanyUserDetail();
                //GetCompanyUserAttachFile();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //if (hdfRoleNo.Value.Trim().Equals("1"))
            //{
            //    Response.Redirect("~/Form/CompanyUserDetail.aspx?CompanyNo=" + hdfCompanyNo.Value.Trim(), true);
            //}
            //else
            //{
            //    Response.Redirect("~/Form/CompanyUserDetail.aspx", true);
            //}
        }

        private void GetCompanyUserDetail()
        {
            try
            {
                Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                MAS_BIDDINGCOMPANY ret = new MAS_BIDDINGCOMPANY();
                ret = manage.GetBiddingCompany(hdfCompanyNo.Value.ToString());
                if (ret != null && ret.CompanyNo > 0)
                {
                    ddlUserType.SelectedValue = ret.CompanyType;
                    txtCompanyName.Text = ret.CompanyName;
                    txtTaxID.Text = ret.TaxID;
                    txtCompanyAdd.Text = ret.CompanyAddress;
                    txtContactPerson.Text = ret.ContactName;
                    txtMobilePhone.Text = ret.MobilePhoneNo;
                    txtPhoneNo.Text = ret.TelephoneNo;
                    lblEmail.Text = ret.Email;
                    txtEmailCC.Text = ret.EmailCC;

                    txtCompanyWebsite.Text = ret.CompanyWebsite;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    MAS_BIDDINGCOMPANY data = new MAS_BIDDINGCOMPANY();

                    /******** Email ********************************/
                    data.Email = lblEmail.Text.Trim();
                    /***********************************************/

                    data.CompanyName = txtCompanyName.Text.Trim();
                    data.TaxID = txtTaxID.Text.Trim();
                    data.CompanyAddress = txtCompanyAdd.Text.Trim();
                    data.ContactName = txtContactPerson.Text.Trim();
                    data.MobilePhoneNo = txtMobilePhone.Text.Trim();
                    data.TelephoneNo = txtPhoneNo.Text.Trim();
                    data.EmailCC = txtEmailCC.Text.Trim();
                    data.CompanyWebsite = txtCompanyWebsite.Text.Trim();

                    data.UpdatedDate = DateTime.Now;
                    data.UpdatedBy = hdfUserName.Value.Trim();
                    data.CompanyNo = Int64.Parse(hdfCompanyNo.Value.Trim());
                    data.CompanyType = ddlUserType.SelectedValue.Trim();

                    /******************** Update Company Attach ****************/
                    string _companyNo = hdfCompanyNo.Value.ToString().Trim();
                    List<MAS_COMPANYATTACHMENT> lAttach = new List<MAS_COMPANYATTACHMENT>();
                    lAttach = SetCompanyFileUpload(_companyNo);
                    /************************************************************/

                    Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                    bool ret = manage.UpdateInfBiddingCompany(data);
                    if (ret)
                    {
                        if (lAttach != null && lAttach.Count > 0)
                        {
                            bool retUpdateAttach = manage.UpdateCompanyAttach(lAttach, _companyNo);
                            /***********************************************************/
                            if (retUpdateAttach)
                            {
                                Session["UpdResult"] = "y";
                                lblMsgResult.Text = "แก้ไขข้อมูลสำเร็จ";
                            }
                            else
                            {
                                logger.Error("Function UpdateCompanyAttach: False");
                                Session["UpdResult"] = "n";
                                lblMsgResult.Text = "ไม่สามารถแก้ไขข้อมูลได้! กรุณาติดต่อผู้ดูแลระบบ";
                            }
                        }
                        else
                        {
                            Session["UpdResult"] = "y";
                            lblMsgResult.Text = "แก้ไขข้อมูลสำเร็จ";
                        }
                    }
                    else
                    {
                        logger.Error("Function [UpdateInfBiddingCompany]: Return False ");
                        Session["UpdResult"] = "n";
                        lblMsgResult.Text = "ไม่สามารถแก้ไขข้อมูลได้! กรุณาติดต่อผู้ดูแลระบบ";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                Session["UpdResult"] = "n";
                lblMsgResult.Text = "ระบบมีปัญหา! กรุณาติดต่อผู้ดูแลระบบ";
            }

            lbtnPopup_ModalPopupExtender.Show();
        }

        private void ClearControl()
        {
            txtCompanyName.Text = string.Empty;
            txtTaxID.Text = string.Empty;
            txtCompanyAdd.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtMobilePhone.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtEmailCC.Text = string.Empty;

            fuCompanyCert.Controls.Clear();
            fuIDCard.Controls.Clear();
            fuVat20.Controls.Clear();
        }

        protected void ValidateTxt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (ddlUserType.SelectedIndex == 0)
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุประเภทผู้เข้าร่วมประมูล";
                    args.IsValid = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล";
                    args.IsValid = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTaxID.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุเลขที่ผู้เสียภาษี";
                    args.IsValid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtCompanyAdd.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุที่อยู่บริษัท";
                    args.IsValid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุชื่อผุ้ติดต่อ";
                    args.IsValid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMobilePhone.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุเบอร์โทรศัพท์มือถือ";
                    args.IsValid = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPhoneNo.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุเบอร์โทรศัพท์สำนักงาน";
                    args.IsValid = false;
                    return;
                }

                #region #### Not Use ####
                /********************Validate Attach File *********************/
                //if (ddlUserType.SelectedValue.ToString().Trim().Equals("1"))
                //{
                //    if (!fuCompanyCert.HasFile || !fuVat20.HasFile)
                //    {
                //        ValidateTxt.ErrorMessage = "กรุณาแนบไฟล์หนังสือรับรองบริษัท และ ภพ.20";
                //        args.IsValid = false;
                //        return;
                //    }
                //}
                //else
                //{
                //    if (!fuIDCard.HasFile)
                //    {
                //        ValidateTxt.ErrorMessage = "กรุณาแนบไฟล์บัตรประจำตัวประชาชน";
                //        args.IsValid = false;
                //        return;
                //    }
                //}
                /**************************************************************/

                /********************Validate is Exist USer ******************/
                //Mas_BiddingCompany_Manage compBL = new Mas_BiddingCompany_Manage();
                //bool isExistComp = compBL.IsExistCompany(txtTaxID.Text.Trim());
                //if (isExistComp)
                //{
                //    ValidateTxt.ErrorMessage = "บริษัท/บุคคลผู้เข้าร่วมประมูลนี้ลงทะเบียนในระบบแล้ว";
                //    args.IsValid = false;
                //    return; 
                //}
                /**************************************************************/
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }       

        private List<MAS_COMPANYATTACHMENT> SetCompanyFileUpload(string CompanyNo)
        {
            List<MAS_COMPANYATTACHMENT> lCompanyAttach = new List<MAS_COMPANYATTACHMENT>();
            try
            {
                string strPathFile = ConfigurationManager.GetConfiguration().AttachFilePath;
                string strPathDate = DateTime.Now.ToString("ddMMyyyy");
                string strUploadFolder = ConfigurationManager.GetConfiguration().CompanyUploadFolder;
                string pathUpload = strPathFile + strUploadFolder + "/" + strPathDate + "/" + CompanyNo + "/";

                /**************** Upload File To Server ***********************/
                if (fuCompanyCert.HasFile || fuVat20.HasFile || fuIDCard.HasFile)
                {
                    if (!System.IO.Directory.Exists(Server.MapPath(pathUpload)))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(pathUpload));
                    }
                }

                if (fuCompanyCert.HasFile)
                {
                    //string fileName = fuCompanyCert.FileName;
                    //String ServerMapPath = Server.MapPath(pathUpload);
                    //fuCompanyCert.PostedFile.SaveAs(ServerMapPath + fuCompanyCert.FileName);

                    string fileName = Path.GetFileName(fuCompanyCert.PostedFile.FileName);
                    //Save files to disk
                    fuCompanyCert.SaveAs(Server.MapPath(pathUpload + fileName));


                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data.CompanyNo = Int64.Parse(CompanyNo);
                    data.AttachFilePath = pathUpload + fileName;
                    data.FileName = fileName;
                    data.Description = "หนังสือรับรองบริษัท";
                    data.CreatedBy = hdfUserName.Value.Trim();
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = hdfUserName.Value.Trim();
                    data.UpdatedDate = DateTime.Now;

                    lCompanyAttach.Add(data);
                }

                if (fuVat20.HasFile)
                {
                    //string fileName = fuVat20.FileName;
                    //String ServerMapPath = Server.MapPath(pathUpload);
                    //fuCompanyCert.PostedFile.SaveAs(ServerMapPath + fuVat20.FileName);


                    string fileName = Path.GetFileName(fuVat20.PostedFile.FileName);
                    //Save files to disk
                    fuVat20.SaveAs(Server.MapPath(pathUpload + fileName));

                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data.CompanyNo = Int64.Parse(CompanyNo);
                    data.AttachFilePath = pathUpload + fileName;
                    data.FileName = fileName;
                    data.Description = "เอกสาร ภพ.20";
                    data.CreatedBy = hdfUserName.Value.Trim();
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = hdfUserName.Value.Trim();
                    data.UpdatedDate = DateTime.Now;

                    lCompanyAttach.Add(data);
                }

                if (fuIDCard.HasFile)
                {
                    //string fileName = fuIDCard.FileName;
                    //String ServerMapPath = Server.MapPath(pathUpload);
                    //fuCompanyCert.PostedFile.SaveAs(ServerMapPath + fuIDCard.FileName);

                    string fileName = Path.GetFileName(fuIDCard.PostedFile.FileName);
                    //Save files to disk
                    fuIDCard.SaveAs(Server.MapPath(pathUpload + fileName));

                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data.CompanyNo = Int64.Parse(CompanyNo);
                    data.AttachFilePath = pathUpload + fileName;
                    data.FileName = fileName;
                    data.Description = "บัตรประจำตัวประชาชน";
                    data.CreatedBy = hdfUserName.Value.Trim();
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = hdfUserName.Value.Trim();
                    data.UpdatedDate = DateTime.Now;

                    lCompanyAttach.Add(data);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return lCompanyAttach;
        }

        #region #### For Download AttachFile (Not Use) ####

        //private void GetCompanyUserAttachFile()
        //{
        //    Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
        //    List<MAS_COMPANYATTACHMENT> ret = new List<MAS_COMPANYATTACHMENT>();
        //    ret = manage.GetCompanyUserAttachFile(hdfCompanyNo.Value.ToString());

        //    if (ret != null && ret.Count > 0)
        //    {
        //        foreach (var item in ret)
        //        {
        //            if (item.Description.Trim().Equals("หนังสือรับรองบริษัท"))
        //            {
        //                lnkDownloadCert.Text = item.Description;
        //                lnkDownloadCert.CommandArgument = item.AttachFilePath;
        //            }

        //            if (item.Description.Trim().Equals("เอกสาร ภพ.20"))
        //            {
        //                lnkDownloadVat20.Text = item.Description;
        //                lnkDownloadVat20.CommandArgument = item.AttachFilePath;
        //            }

        //            if (item.Description.Trim().Equals("บัตรประจำตัวประชาชน"))
        //            {
        //                lnkDownloadIDCard.Text = item.Description;
        //                lnkDownloadIDCard.CommandArgument = item.AttachFilePath;
        //            }
        //        }
        //    }
        //}

        //protected void lnkDownloadVat20_Click(object sender, EventArgs e)
        //{
        //    string _pathFile = lnkDownloadVat20.CommandArgument;
        //    DownloadFile( _pathFile);
        //}

        //protected void lnkDownloadCert_Click(object sender, EventArgs e)
        //{
        //    string _pathFile = lnkDownloadCert.CommandArgument;
        //    DownloadFile(_pathFile);
        //}

        //protected void lnkDownloadIDCard_Click(object sender, EventArgs e)
        //{
        //    string _pathFile = lnkDownloadIDCard.CommandArgument;
        //    DownloadFile(_pathFile);
        //}

        //private void DownloadFile(string pathFile)
        //{
        //    string fileName = Path.GetFileName(pathFile);

        //    WebClient req = new WebClient();
        //    HttpResponse response = HttpContext.Current.Response;
        //    response.Clear();
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Buffer = true;
        //    response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        //    byte[] data = req.DownloadData(Server.MapPath(pathFile));
        //    response.BinaryWrite(data);
        //    response.End();
        //}
        #endregion

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //string sResult = (string)Session["UpdResult"];
                //if (sResult.Trim().Equals("y"))
                //{
                //    lbtnPopup_ModalPopupExtender.Hide();
                //    if (hdfRoleNo.Value.Trim().Equals("1"))
                //    {
                //        Response.Redirect("~/Form/CompanyUserDetail.aspx?CompanyNo=" + hdfCompanyNo.Value.Trim(), true);
                //    }
                //    else
                //    {
                //        Response.Redirect("~/Form/CompanyUserDetail.aspx", true);
                //    }
                //}
                //else
                //{
                //    lbtnPopup_ModalPopupExtender.Hide();
                //}

                lbtnPopup_ModalPopupExtender.Hide();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
    }
}
