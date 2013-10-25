using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "文件不存在！请输入正确文件名！";
        }
        public Form1(string lj)//无参数情况
        {
            InitializeComponent();
           

            string str = File.ReadAllText(lj); 
            int[,] a=new int[100,100];
            string[] zj;
            int[,,] b=new int[50,50,50];
            int i = 0, j = 0, k = 0, m = 0, n = 0,ans=0,temp=0,p=0;
            int up=0,down=0,left=0,right=0,left1=0;
           
            zj = str.Split(' ','\n');
            m = int.Parse(zj[0]);
            n = int.Parse(zj[1]);//获取矩阵的长和款
            p=2;//从zj数组第三个元素开始记录矩阵
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    a[i,j] = int.Parse(zj[p]);
                    p++;
                }
            }
            
            //设置表格的长和宽
            dataGridView1.RowCount = m;
            dataGridView1.ColumnCount = n;
            
            //表格属性设置
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            
            for (j = 0; j < n;j++ )
                dataGridView1.Columns[j].Width = 30;
        
            //对个单元格赋值
            for (i = 0; i < m; i++)
                for (j = 0; j < n;j++ )
                    dataGridView1[j, i].Value = a[i, j];

            //使用算法计算出最大矩形子数组
             ans = a[0,0];
				for (k = 0; k < n; k++)
				{
					for (i = 0; i < m; i++)
					{
						temp = 0;
						for (j = i; j < m; j++)
						{
							temp = temp + a[j,k];
							b[i,j,k] = temp;
						}
					}
				}
				for (i = 0; i < m; i++)
				for (j = i; j < m; j++)
				{
					temp = 0;
					left1 = 0;
					for (k = 0; k < n; k++)
					{
						temp = temp + b[i,j,k];
						if (temp > ans)
						{
							ans = temp;
							up = i; down = j; left = left1; right = k;
						}
						else if (temp <= 0) { temp = 0; left1=k+1; }
						
					}
				}
                
                //对子数组进行染色
                for (i = up; i <= down; i++)
                    for (j = left; j <= right; j++)
                        dataGridView1[j, i].Style.BackColor = Color.Red;

                label1.Text = "当前矩阵的最大字数组和为" + ans;

        }
        public Form1(string cs,string lj)//有参数情况
        {
            InitializeComponent();
            
            
            if (cs.Equals("/v"))//上下相连情况
            {
                string str = File.ReadAllText(lj);
                int[,] a = new int[100, 100];
                string[] zj;
                int[, ,] b = new int[50, 50, 50];
                int i = 0, j = 0, k = 0, m = 0, n = 0, ans = 0, temp = 0, p = 0;
                int up = 0, down = 0, left = 0, right = 0, left1 = 0;

                zj = str.Split(' ', '\n');
                m = int.Parse(zj[0]);
                n = int.Parse(zj[1]);//获取矩阵的长和款
                p = 2;//从zj数组第三个元素开始记录矩阵
                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        a[i, j] = int.Parse(zj[p]);
                        a[i + m, j] = a[i, j];
                        p++;
                    }
                }

                //设置表格的长和宽
                dataGridView1.RowCount = m;
                dataGridView1.ColumnCount = n;

                //表格属性设置
                dataGridView1.ReadOnly = true;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                for (j = 0; j < n; j++)
                    dataGridView1.Columns[j].Width = 30;

                //对个单元格赋值
                for (i = 0; i < m; i++)
                    for (j = 0; j < n; j++)
                        dataGridView1[j, i].Value = a[i, j];
                //调用算法计算出最大矩形子数组
                ans = a[0,0];
                for (k = 0; k < n; k++)
                {
                    for (i = 0; i < m * 2; i++)
                    {
                        temp = 0;
                        for (j = i; j < m * 2; j++)
                        {
                            temp = temp + a[j,k];
                            b[i,j,k] = temp;
                        }
                    }
                }
                for (i = 0; i < m * 2; i++)
                    for (j = i; j < m * 2; j++)
                    {
                        temp = 0;
                        left1 = 0;//每次循环前左界限要置为0
                        for (k = 0; k < n; k++)
                        {
                            temp = temp + b[i,j,k];
                            if (temp > ans && j - i < m)
                            {
                                ans = temp;
                                up = i; down = j; left = left1; right = k;


                            }
                            else if (temp <= 0 && j - i < m)
                            {
                                temp = 0;
                                left1 = k + 1;

                            }

                        }
                    }
                //对子数组进行染色
                for (i = up; i <= down; i++)
                    for (j = left; j <= right; j++)
                    {
                        
                        dataGridView1[j, i%m].Style.BackColor = Color.Red;
                    }
                 
                label1.Text = "当前矩阵的最大字数组和为" +ans+"(上下联通)";
                         
            }
            
            else if (cs.Equals("/h"))//左右相连情况
            {
                string str = File.ReadAllText(lj);
                int[,] a = new int[100, 100];
                string[] zj;
                int[, ,] b = new int[50, 50, 50];
                int i = 0, j = 0, k = 0, m = 0, n = 0, ans = 0, temp = 0, p = 0;
                int up = 0, down = 0, left = 0, right = 0, left1 = 0;

                zj = str.Split(' ', '\n');
                m = int.Parse(zj[0]);
                n = int.Parse(zj[1]);//获取矩阵的长和款
                p = 2;//从zj数组第三个元素开始记录矩阵
                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        a[i, j] = int.Parse(zj[p]);
                        a[i, j + n] = a[i, j];//在原矩阵右面补一个相同的矩阵
                        p++;
                    }
                }

                //设置表格的长和宽
                dataGridView1.RowCount = m;
                dataGridView1.ColumnCount = n;

                //表格属性设置
                dataGridView1.ReadOnly = true;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                for (j = 0; j < n; j++)
                    dataGridView1.Columns[j].Width = 30;

                //对个单元格赋值
                for (i = 0; i < m; i++)
                    for (j = 0; j < n; j++)
                        dataGridView1[j, i].Value = a[i, j];
                //调用算法计算出最大矩形子数组
                ans = a[0, 0];
                for (k = 0; k < n*2; k++)
                {
                    for (i = 0; i < m ; i++)
                    {
                        temp = 0;
                        for (j = i; j < m ; j++)
                        {
                            temp = temp + a[j, k];
                            b[i, j, k] = temp;
                        }
                    }
                }
                for (i = 0; i < m ; i++)
                    for (j = i; j < m ; j++)
                    {
                        temp = 0;
                        left1 = 0;//每次循环前左界限要置为0
                        for (k = 0; k < n*2; k++)
                        {
                            temp = temp + b[i, j, k];
                            if (temp > ans && k - left1 < n)
                            {
                                ans = temp;
                                up = i; down = j; left = left1; right = k;


                            }
                            else if (temp <= 0 && k - left1 < n)
                            {
                                temp = 0;
                                left1 = k + 1;

                            }

                        }
                    }
                //对子数组进行染色
                for (i = up; i <= down; i++)
                    for (j = left; j <= right; j++)
                    {

                        dataGridView1[j%n, i].Style.BackColor = Color.Red;
                    }

                label1.Text = "当前矩阵的最大字数组和为" + ans + "(左右联通)";

            }
         
        }
    }
}
