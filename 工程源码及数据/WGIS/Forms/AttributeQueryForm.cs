using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase; 


namespace WGIS.Forms
{
    public partial class AttributeQueryForm : Form
    {
        private AxMapControl mMapControl;
        //选中图层 
        private IFeatureLayer mFeatureLayer;
        //根据所选择的图层查询得到的特征类
        private IFeatureClass pFeatureClass = null;

        public AttributeQueryForm(AxMapControl mapControl)
        {
            InitializeComponent();
            this.mMapControl = mapControl;
        }

        /// <summary>
        /// 加载属性查询窗口
        /// </summary>
        private void AttributeQueryForm_Load(object sender, EventArgs e)
        {
            //MapControl中没有图层时返回 
            if (this.mMapControl.LayerCount <= 0)
                return;
            //获取MapControl中的全部图层名称，并加入ComboBox 
            //图层 
            ILayer pLayer;
            //图层名称 
            string strLayerName;
            for (int i = 0; i < this.mMapControl.LayerCount; i++)
            {
                pLayer = this.mMapControl.get_Layer(i);
                strLayerName = pLayer.Name;
                //图层名称加入cboLayer 
                this.cboLayer.Items.Add(strLayerName);
            }
            //默认显示第一个选项 
            this.cboLayer.SelectedIndex = 0;

        }

        /// <summary>
        /// 获取可选的所有图层
        /// </summary>
        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空listBoxField控件的内容
            this.listBoxField.Items.Clear();
            //获取cboLayer中选中的图层 
            mFeatureLayer = mMapControl.get_Layer(cboLayer.SelectedIndex) as IFeatureLayer;
            pFeatureClass = mFeatureLayer.FeatureClass;
            //字段名称 
            string strFldName;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                strFldName = pFeatureClass.Fields.get_Field(i).Name;
                //图层名称加入cboField 
                this.listBoxField.Items.Add(strFldName);
            }
            //默认显示第一个选项 
            this.listBoxField.SelectedIndex = 0;

        }

        /// <summary>
        /// 获取图层中可选的所有字段
        /// </summary>
        private void listBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sFieldName = listBoxField.Text;
            listBoxValue.Items.Clear();
            int iFieldIndex = 0;
            IField pField = null;
            IFeatureCursor pFeatCursor = pFeatureClass.Search(null, true);
            IFeature pFeat = pFeatCursor.NextFeature();
            iFieldIndex = pFeatureClass.FindField(sFieldName);
            pField = pFeatureClass.Fields.get_Field(iFieldIndex);
            while (pFeat != null)
            {
                listBoxValue.Items.Add(pFeat.get_Value(iFieldIndex));
                pFeat = pFeatCursor.NextFeature();
            }
        }

        /// <summary>
        /// 获取选择的字段
        /// </summary>
        private void listBoxField_DoubleClick(object sender, EventArgs e)
        {
            //数据类型为shapefile
            textBoxSql.SelectedText = "\"" + listBoxField.SelectedItem.ToString() + "\"" + " ";
        }

        /// <summary>
        /// 获取选择的字段值
        /// </summary>
        private void listBoxValue_DoubleClick(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "'" + listBoxValue.SelectedItem.ToString() + "'" + " ";
        }

        #region 表达式button响应
        private void btnequal_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "= ";
        }

        private void btnunequal_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "!= ";
        }

        private void btnis_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "is ";
        }

        private void btnlike_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "like ";
        }

        private void btnmore_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "> ";
        }

        private void btnless_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "< ";
        }


        private void btnmoe_Click_1(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = ">= ";
        }

        private void btnloe_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "<= ";
        }

        private void btnor_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "or ";
        }

        private void btnnull_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "null ";
        }

        private void btncharacter_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "'' ";
        }

        private void btnbetween_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "between ";
        }

        private void btnspace_Click(object sender, EventArgs e)
        {
            textBoxSql.SelectedText = "' ' ";
        }

        //清空查询表达式对话框
        private void btnempty_Click(object sender, EventArgs e)
        {
            this.textBoxSql.SelectedText = "";
        }

        #endregion

        /// <summary>
        /// 开始查询
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                mMapControl.Map.ClearSelection(); //清除上次查询结果
                IActiveView pActiveView = mMapControl.Map as IActiveView;
                //pQueryFilter的实例化 
                IQueryFilter pQueryFilter = new QueryFilterClass();
                //设置查询过滤条件 
                pQueryFilter.WhereClause = textBoxSql.Text;
                //查询 ,search的参数第一个为过滤条件，第二个为是否重复执行
                IFeatureCursor pFeatureCursor = mFeatureLayer.Search(pQueryFilter, false);
                //获取查询到的要素 
                IFeature pFeature = pFeatureCursor.NextFeature();
                //判断是否获取到要素 
                while (pFeature != null)
                {
                    mMapControl.Map.SelectFeature(mFeatureLayer, pFeature); //选择要素 
                    mMapControl.Extent = pFeature.Shape.Envelope; //放大到要素
                    pFeature = pFeatureCursor.NextFeature();
                }
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                pActiveView.Refresh();//刷新图层
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
