using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace AlgoVersion2
{
    public partial class MainForm : Form
    {
        List<Panel> tiles;
        List<Label> label;
        List<int> init;
        List<int> values;
        List<List<int>> list;
        List<List<int>> removedList;
        List<int> removedValues;
        int frame;
        char type;
        bool isMerge;
        public MainForm()
        {
            InitializeComponent();
            removedValues = new List<int>();
            list = new List<List<int>>();
            label = new List<Label>();
            tiles = new List<Panel>();
            removedList = new List<List<int>>();
            isMerge = false;
            init = shuffle(5, 50);
            //MessageBox.Show(init.Count.ToString());
            values = new List<int>();
            isMerge = false;
            for (int x = 0; x < 12; x++)
            {
                values.Add(init[x]);
            }
            trackBar1.Value = 1000;
            button3.ImageIndex = 2;
            progressBar.Value = 0;
             // initializaBubbleSort();
            //initializeSelectionSort();
           // initializeInsertionSort();
           // title.Enabled = true;
            title.ReadOnly = true;
       
            
        }
        private int toInt(string text)
        {
            return Convert.ToInt32(text);
        }
      
        /// showList
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void showList(List<int> list)
        {
            int multiplier = 6;
            int height = 299;
            if (type == 'i')
            {
                multiplier = 4;
                height = 246;
                tilesPanel.Height = 492;
                tilesPanel.Location = new Point(42, 63);
            }
            else
            {
                tilesPanel.Height = 300;
                tilesPanel.Location = new Point(39, 120);
            }
            Label lab;
            Label temp;
            Point cursor = new Point(5, height - list[0] * multiplier);
            tilesPanel.Controls.Clear();
            for (int x = 0; x < 12; x++)
            {

                lab = new Label();
                lab.AutoSize = false;
                if (list[x * 2 + 1] == 1)
                    lab.BackColor = Color.Green;
                else if (list[x * 2 + 1] == 2)
                    lab.BackColor = Color.Gold;
                else if (list[x * 2 + 1] == 3)
                    lab.BackColor = Color.Red;
                else if (list[x * 2 + 1] == 5)
                    lab.BackColor = Color.Violet;
                else
                    lab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                if (list[x * 2 + 1] == 4)
                {
                    lab.Location = new Point(cursor.X, cursor.Y + 40 + list[x * 2] * multiplier);
                    lab.BackColor = Color.Red;
                }
                else
                    lab.Location = cursor;
                lab.Size = new System.Drawing.Size(40, list[x*2] * multiplier);
                lab.TabIndex = 0;

                lab.Text = list[x*2].ToString();
                lab.Font = new Font(this.Font.FontFamily, 10,FontStyle.Bold);
                lab.Padding = new Padding(0, 0, 0, 7);
                lab.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                if(x < 11)
                    cursor = new Point(cursor.X + 50, height - (list[x*2 + 2] * multiplier));
                tilesPanel.Controls.Add(lab);

            }
           // showAlternateList()


        }
        
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void progressBar1_Click(object sender, EventArgs e)
        {
          
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        /// Shuffle
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<int> shuffle(int from, int to)
        {
            List<int> list = new List<int>();
            for (int x = from; x <= to; x++)
            {
                list.Add(x);
            }
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (int.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
        /// Timer
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (frame < list.Count){
                showList(list[frame]);
                if(isMerge)
                    showAlternateList(removedList[frame]);
                frame++;
            }
                
            else if (frame == list.Count)
            {
                this.timer1.Enabled = false;
                //isMerge = false;
                MessageBox.Show("The list is sorted");
            }
                progressBar.Value = frame;
                hScrollBar1.Value = frame;

            
        
           
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /// Play Button
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ImageIndex == 2)
            {
                button3.ImageIndex = 4;
                timer1.Enabled = true;
            }
            else
            {
                button3.ImageIndex = 2;
                timer1.Enabled = false;
            }
        }
        ///TrackBall
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
        }
        ///HScrollBar
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (welcomePanel.Visible == false)
            {
                timer1.Enabled = false;
                frame = hScrollBar1.Value - 1;
                if (frame < 0)
                    frame = 0;
                if (frame > list.Count - 1)
                    frame = list.Count - 1;
                showList(list[frame]);
                if (isMerge)
                    showAlternateList(removedList[frame]);
                button3.ImageIndex = 2;
            }
            if (hScrollBar1.Value < progressBar.Maximum && hScrollBar1.Value > progressBar.Minimum)
                progressBar.Value = hScrollBar1.Value;
            else if (hScrollBar1.Value > progressBar.Value)
                progressBar.Value = progressBar.Maximum;
            else
                progressBar.Value = progressBar.Minimum;
        }
        ///Next Button
        private void button5_Click(object sender, EventArgs e)
        {
            if (welcomePanel.Visible == false)
            {
                timer1.Enabled = false;
                button3.ImageIndex = 2;
                frame++;
                if (frame > list.Count - 1)
                    frame = list.Count - 1;

                showList(list[frame]);
                if (isMerge)
                    showAlternateList(removedList[frame]);
            }
        }
        //Prev button
        private void button2_Click(object sender, EventArgs e)
        {
            if (welcomePanel.Visible == false)
            {
                timer1.Enabled = false;
                button3.ImageIndex = 2;
                frame--;
                if (frame < 0)
                    frame = 0;
                showList(list[frame]);
                if (isMerge)
                    showAlternateList(removedList[frame]);
            }
        }
        //InitializeTracker
        private void initializeTracker()
        {
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = list.Count;

            hScrollBar1.Value = 0;
            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = list.Count + 10;
        }




        /// SelectionSort
        /// 
        /// </summary>
        private void initializeSelectionSort()
        {
            isMerge = false;
            timer1.Enabled = false;
            init = shuffle(5, 50);
            //MessageBox.Show(init.Count.ToString());
            values = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                values.Add(init[x]);
            }
            list.Clear();
            int smallest = values[0];
            int smallestIndex = 0;
            List<int> holder;
            int temp;
           
            for (int x = 0; x < 12; x++)
            {
                smallestIndex = x;
                smallest = values[x];
                holder = new List<int>();
                for (int z = 0; z < 12; z++)
                {
                    holder.Add(values[z]);
                    if (z < x)
                        holder.Add(2);
                    else if (z == smallestIndex)
                        holder.Add(3);
                    else
                        holder.Add(0);
                }
                list.Add(holder);
                for (int y = x+1; y < 12; y++)
                {
                    holder = new List<int>();
                    for (int z = 0; z < 12; z++)
                    {
                        holder.Add(values[z]);
                        if (z < x)
                            holder.Add(2);
                        else if (z == smallestIndex)
                            holder.Add(3);
                        else if (z == y)
                            holder.Add(1);
                        else
                            holder.Add(0);
                    }
                    list.Add(holder);
                    if (values[y] < smallest)
                    {
                        smallest = values[y];
                        smallestIndex = y;
                        holder = new List<int>();
                        for (int z = 0; z < 12; z++)
                        {
                            holder.Add(values[z]);
                            if (z < x)
                                holder.Add(2);
                            else if (z == smallestIndex)
                                holder.Add(3);
                            else
                                holder.Add(0);
                        }
                        list.Add(holder);
                    }
                   
                }

                holder = new List<int>();
                for (int z = 0; z < 12; z++)
                {
                    holder.Add(values[z]);
                    if (z < x)
                        holder.Add(2);
                    else if (z == smallestIndex || z == x)
                        holder.Add(3);
                    else
                        holder.Add(0);
                }
                list.Add(holder);
                temp = values[smallestIndex];
                values[smallestIndex] = values[x];
                values[x] = temp;

                holder = new List<int>();
                for (int z = 0; z < 12; z++)
                {
                    holder.Add(values[z]);
                    if (z < x)
                        holder.Add(2);
                    else if (z == smallestIndex || z == x)
                        holder.Add(3);
                    else
                        holder.Add(0);
                }
                list.Add(holder);
                holder = new List<int>();
                for (int z = 0; z < 12; z++)
                {
                    holder.Add(values[z]);
                    if (z < x)
                        holder.Add(2);
                    else if (z == x)
                        holder.Add(2);
                    else
                        holder.Add(0);
                }
                list.Add(holder);

                if ((x+1) == 11)
                {
                    holder = new List<int>();
                    for (int z = 0; z < 12; z++)
                    {
                        holder.Add(values[z]);
                        holder.Add(2);
                    }
                    list.Add(holder);
                    break;
                }
                


            }

            frame = 0;
            timer1.Interval = trackBar1.Value;
            //timer1.Enabled = true;
            showList(list[0]);
            initializeTracker();

        }
        private void selectionSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.ImageIndex = 2;
            welcomePanel.Visible = false;
            title.Text = "Selection Sort";
            initializeSelectionSort();
        }



        /// InsertionSort
        /// 
        /// </summary>
        private void initializeInsertionSort()
        {
            isMerge = false;
            timer1.Enabled = false;
            init = shuffle(5, 50);
            //MessageBox.Show(init.Count.ToString());
            values = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                values.Add(init[x]);
            }
            list.Clear();
            List<int> holder;
            int temp;
            int indexToFollow = 0;
            ///Initialize --
            /// 
            /// 
            holder = new List<int>();
            for (int z = 0; z < 12; z++)
            {
                holder.Add(values[z]);
                holder.Add(0);
            }
            list.Add(holder);

           
            ///  set first element as sorted
            /// 
            holder = new List<int>();
            for (int z = 0; z < 12; z++)
            {
                holder.Add(values[z]);
                if (z == indexToFollow)
                    holder.Add(2); //GOLD
                else
                    holder.Add(0);
            }
            list.Add(holder);


            ///Starting of the LOOP
            for (int x = 1; x < 12; x++)
            {
                indexToFollow = x;
                ///Invert indextoFollow and set color to red
                ///
                holder = new List<int>();
                for (int z = 0; z < 12; z++)
                {
                    holder.Add(values[z]);
                    if (z < x)
                        holder.Add(2); // GOLD
                    else if (z == indexToFollow)
                        holder.Add(4); //Red
                    else
                        holder.Add(0);
                }
                list.Add(holder);
                ///Inner LOOP
                for (int y = x - 1; y >= 0; y--)
                {
                    holder = new List<int>();
                    for (int z = 0; z < 12; z++)
                    {
                        holder.Add(values[z]);
                        if (z == y)
                            holder.Add(1); //Green
                        else if (z == indexToFollow)
                            holder.Add(4); //Red
                        else if (z <= x)
                            holder.Add(2);//Gold
                        else
                            holder.Add(0);
                    }
                    list.Add(holder);
                    if (values[indexToFollow] >= values[y])
                        break;
                    ///Condition for Swapping
                    if (values[indexToFollow] < values[y])
                    {
                        temp = values[indexToFollow];
                        values[indexToFollow] = values[y];
                        values[y] = temp;
                        holder = new List<int>();
                        for (int z = 0; z < 12; z++)
                        {
                            holder.Add(values[z]);
                            if (z == indexToFollow)
                                holder.Add(2); //Gold
                            else if (z == y)
                                holder.Add(4); //Red
                            else if (z <= x)
                                holder.Add(2); //Gold
                            else
                                holder.Add(0);
                        }
                        list.Add(holder);
                        indexToFollow = y;
                    }
                    //holder = new List<int>();
                    //for (int z = 0; z < 12; z++)
                    //{
                    //    holder.Add(values[z]);
                    //    if (z == y )
                    //        holder.Add(1); //Green
                    //    else if (z == indexToFollow)
                    //        holder.Add(4); //Red
                    //    else if (z < x)
                    //        holder.Add(2);//Gold
                    //    else
                    //        holder.Add(0);
                    //}
                    //list.Add(holder);
                }///Closing for inner loop
                holder = new List<int>();
                for (int z = 0; z < 12; z++)
                {
                    holder.Add(values[z]);
                    if (z < (x+1))
                        holder.Add(2);//Gold
                    else
                        holder.Add(0);
                }
                list.Add(holder);
            } /// Closing for starting  loop
            frame = 0;
            type = 'i';
            
            timer1.Interval = trackBar1.Value;
            //timer1.Enabled = true;
            showList(list[0]);
            initializeTracker();
           // timer1.Enabled = true;
           
        }
        private void insertionSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.ImageIndex = 2;
            welcomePanel.Visible = false;
            title.Text = "Insertion Sort";
            initializeInsertionSort();
        }


        /// Initialize the BubbleSort 
        /// 
        /// 
        /// </summary>
        public void initializaBubbleSort()
        {
            isMerge = false;
            timer1.Enabled = false;
            init = shuffle(5, 50);
            //MessageBox.Show(init.Count.ToString());
            values = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                values.Add(init[x]);
            }
            list.Clear();
            bool flag = false;
            int temp;
            int counter = 1;
            List<int> holder;
            while (!flag)
            {
                flag = true;
                for (int x = 0; x < 12 - counter; x++)
                {
                    if (values[x] > values[x + 1])
                    {
                        holder = new List<int>();
                        for (int y = 0; y < 12; y++)
                        {
                            holder.Add(values[y]);
                            if (y >= 12 - (counter - 1))
                                holder.Add(2);
                            else if (x == y || y == x + 1)
                                holder.Add(1);
                            else
                                holder.Add(0);
                        }
                        // MessageBox.Show("Holder count:  "+holder.Count + "");
                        list.Add(holder);
                        temp = values[x];
                        values[x] = values[x + 1];
                        values[x + 1] = temp;
                        flag = false;



                    }
                    holder = new List<int>();
                    for (int y = 0; y < 12; y++)
                    {
                        holder.Add(values[y]);
                        if (y >= 12 - (counter - 1))
                            holder.Add(2); //gold
                        else if (x == y || y == x + 1)
                            holder.Add(1);//Green
                        else
                            holder.Add(0);
                    }
                    // MessageBox.Show("Holder count:  "+holder.Count + "");
                    list.Add(holder);


                }
                holder = new List<int>();
                for (int y = 0; y < 12; y++)
                {
                    holder.Add(values[y]);
                    if (y >= 11 - (counter - 1))
                        holder.Add(2);
                    else
                        holder.Add(0);
                }
                list.Add(holder);
                counter++;

            }
            holder = new List<int>();
            for (int y = 0; y < 12; y++)
            {
                holder.Add(values[y]);
                holder.Add(2);
            }
            list.Add(holder);
            frame = 0;
            timer1.Interval = trackBar1.Value;
            type = 'b';
            //timer1.Enabled = true;
            showList(list[0]);
            initializeTracker();


        }
        private void bubbleSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.ImageIndex = 2;
            welcomePanel.Visible = false;
            title.Text = "Bubble Sort";
            initializaBubbleSort();
        }
       



        ///MergeSORT
        /// 
        /// </summary>
        private void initializeMerge()
        {

            timer1.Enabled = false;
            isMerge = true;
            init = shuffle(5, 50);
            //MessageBox.Show(init.Count.ToString());
            removedList.Clear();
            values = new List<int>();
            removedValues = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                removedValues.Add(0);
                values.Add(init[x]);
            }
            list.Clear();
            mergeSort(values,0, 11);
            frame = 0;
            timer1.Interval = trackBar1.Value;
            type = 'i';
            //timer1.Enabled = true;
            showList(list[0]);
            initializeTracker();
        }
        public void mergeSort(List<int> lists,int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;

                mergeSort(lists,l, m);
                mergeSort(lists,m + 1, r);

                merge(lists,l, m, r);
            }
            if (r - l == 11)
            {
                List<int> temp;
                temp = new List<int>();
                for (int x = 0; x < 12; x++)
                {
                    temp.Add(values[x]);
                    temp.Add(2);
                }
                list.Add(temp);

                temp = new List<int>();
                for (int x = 0; x < 12; x++)
                {
                    temp.Add(0);
                    temp.Add(1);
                }
                removedList.Add(temp);
            }
        }
        public void merge(List<int> lists, int l, int m, int r)
        {
          // MessageBox.Show("Merge");
            List<int> holder = new List<int>();
            List<int> tempValues = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                tempValues.Add(values[x]);
            }

            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            List<int> temp;
            List<int> tempAlternate;
            /* create temp arrays */
            int[] L = new int[n1];
            int[] R = new int[n2];
         
            /* Copy data to temp arrays L[] and R[] */
            for (i = 0; i < n1; i++)
                L[i] = values[l + i];
            for (j = 0; j < n2; j++)
                R[j] = values[m + 1 + j];
            //Main List
            

            ///Use to track the index the values list
            int leftIndex = l;
            int rightIndex = m + 1;

            // use to track the position of removed list
            int removed = l;
            int withValues = 0;

            ///Get Snapshot of mainlist
            temp = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                temp.Add(values[x]);
                if (x >= l && x <= r)
                    temp.Add(3);
                else
                    temp.Add(0);
            }
            list.Add(temp);
            ///Get snapshot of remove list
            tempAlternate = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                tempAlternate.Add(0);
                tempAlternate.Add(1);
            }
            removedList.Add(tempAlternate);


            /* Merge the temp arrays back into arr[l..r]*/
            i = 0; // Initial index of first subarray
            j = 0; // Initial index of second subarray
            k = l; // Initial index of merged subarray
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {

                    ///Get Snapshot of mainlist
                    temp = new List<int>();
                    for (int x = 0; x < 12; x++)
                    {
                        if (x == leftIndex || (x < leftIndex && x >= l ) || (x < rightIndex && x >= m + 1 ))
                        {
                            temp.Add(0);
                        }
                        else
                            temp.Add(values[x]);
                        if (x >= l && x <= r)
                            temp.Add(3);
                        else
                        temp.Add(0);
                        
                    }
                    ///Get snapshot of remove list
                    tempAlternate = new List<int>();
                    withValues = 0;
                    for (int x = 0; x < 12; x++)
                    {
                        //if (x < l || x > removed)
                        //    tempAlternate.Add(0);
                        if (x >= l && x < removed)
                            tempAlternate.Add(holder[withValues++]);
                        else if (x == removed)
                            tempAlternate.Add(L[i]);
                        else
                            tempAlternate.Add(0);
                        tempAlternate.Add(1);
                    }
                    removed++;
                    removedList.Add(tempAlternate);
                    leftIndex++;
                    list.Add(temp);
                    holder.Add(L[i]);
                    i++;
                    
                }
                else
                {
                    ///Get Snapshot of mainlist
                    temp = new List<int>();
                    for (int x = 0; x < 12; x++)
                    {
                        if (x == rightIndex || (x < leftIndex && x >= l ) || (x < rightIndex && x >= m + 1 ))
                        {
                            temp.Add(0);
                        }
                        else
                            temp.Add(values[x]);
                        if (x >= l && x <= r)
                            temp.Add(3);
                        else
                            temp.Add(0);
                       
                    }

                    ///Get snapshot of remove list
                    tempAlternate = new List<int>();
                    withValues = 0;
                    for (int x = 0; x < 12; x++)
                    {
                        //if (x < l || x > removed)
                        //    tempAlternate.Add(0);
                        if (x >= l && x < removed)
                            tempAlternate.Add(holder[withValues++]);
                        else if (x == removed)
                            tempAlternate.Add(R[j]);
                        else
                            tempAlternate.Add(0);
                        tempAlternate.Add(1);
                    }
                    removedList.Add(tempAlternate);
                    removed++;
                    holder.Add(R[j]);
                    rightIndex++;
                    list.Add(temp);
                    j++;
                }
                k++;
            }
          
            /* Copy the remaining elements of L[], if there
                are any */
            while (i < n1)
            {
                ///Get Snapshot of mainlist
                temp = new List<int>();
                for (int x = 0; x < 12; x++)
                {
                    if (x == leftIndex || (x < leftIndex && x >= l ) || (x < rightIndex && x >= m + 1 ))
                    {
                        temp.Add(0);
                    }
                    else
                        temp.Add(values[x]);
                   
                    if (x >= l && x <= r)
                        temp.Add(3);
                    else
                    temp.Add(0);
                }
                list.Add(temp);

                ///Get snapshot of remove list
                tempAlternate = new List<int>();
                withValues = 0;
                for (int x = 0; x < 12; x++)
                {
                    //if (x < l || x > removed)
                    //    tempAlternate.Add(0);
                    if (x >= l && x < removed)
                        tempAlternate.Add(holder[withValues++]);
                    else if (x == removed)
                        tempAlternate.Add(L[i]);
                    else
                        tempAlternate.Add(0);
                    tempAlternate.Add(1);
                }
                removed++;
                removedList.Add(tempAlternate);
                holder.Add(L[i]);
               // lists[k] = L[i];
                leftIndex++;
                i++;
                k++;
            }
          //  MessageBox.Show(holder.Count.ToString());

            /* Copy the remaining elements of R[], if there
                are any */
            while (j < n2)
            {
                ///Get Snapshot of mainlist
                temp = new List<int>();
                for (int x = 0; x < 12; x++)
                {
                    if (x == rightIndex || (x < leftIndex && x >= l ) || (x < rightIndex && x >= m + 1 ))
                    {
                        temp.Add(0);
                    }
                    else
                        temp.Add(values[x]);
                    if (x >= l && x <= r)
                        temp.Add(3);
                    else
                    temp.Add(0);
                }
                list.Add(temp);
                ///Get snapshot of remove list
                tempAlternate = new List<int>();
                withValues = 0;
                for (int x = 0; x < 12; x++)
                {
                    //if (x < l || x > removed)
                    //    tempAlternate.Add(0);
                    if (x >= l && x < removed)
                        tempAlternate.Add(holder[withValues++]);
                    else if (x == removed)
                        tempAlternate.Add(R[j]);
                    else
                        tempAlternate.Add(0);
                    tempAlternate.Add(1);
                }
                removedList.Add(tempAlternate);
                removed++;
                holder.Add(R[j]);
               // lists[k] = R[j];
                rightIndex++;
                j++;
                k++;
            }
            int counter = 0;
         // MessageBox.Show(holder.Count.ToString());
            for (int x = l; x < holder.Count+l ; x++)
            {
                values[x] = holder[counter++];

            }
            ///Get Snapshot of mainlist
            temp = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                temp.Add(values[x]);
                if (x >= l && x <= r)
                    temp.Add(3);
                else
                    temp.Add(0);
            }
            list.Add(temp);
            ///Get snapshot of remove list
            tempAlternate = new List<int>();
            for (int x = 0; x < 12; x++)
            {
               
                tempAlternate.Add(0);
                tempAlternate.Add(3);
            }
            removedList.Add(tempAlternate);
            removed++;


        }
        /// Show ALternate List
        /// 
        /// </summary>
        private void showAlternateList(List<int> list)
        {
            int multiplier = 4;
            int height = 446;
            tilesPanel.Height = 492;
            tilesPanel.Location = new Point(42, 63);
            Label lab;
            Point cursor = new Point(5, height - list[0] * multiplier);
            //tilesPanel.Controls.Clear();
            for (int x = 0; x < 12; x++)
            {
                lab = new Label();
                lab.AutoSize = false;
                if (list[x * 2 + 1] == 1)
                    lab.BackColor = Color.Green;
                else if (list[x * 2 + 1] == 2)
                    lab.BackColor = Color.Gold;
                else if (list[x * 2 + 1] == 3)
                    lab.BackColor = Color.Red;
                else if (list[x * 2 + 1] == 5)
                    lab.BackColor = Color.Violet;
                else
                    lab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                if (list[x * 2 + 1] == 4)
                {
                    lab.Location = new Point(cursor.X, cursor.Y + 40 + list[x * 2] * multiplier);
                    lab.BackColor = Color.Red;
                }
                else
                    lab.Location = cursor;
                lab.Size = new System.Drawing.Size(40, list[x * 2] * multiplier);
                lab.TabIndex = 0;

                lab.Text = list[x * 2].ToString();
                lab.Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold);
                lab.Padding = new Padding(0, 0, 0, 7);
                lab.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                if (x < 11)
                    cursor = new Point(cursor.X + 50, height - (list[x * 2 + 2] * multiplier));
                tilesPanel.Controls.Add(lab);

            }
        }
        private void mergeSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.ImageIndex = 2;
            welcomePanel.Visible = false;
            title.Text = "Merge Sort";
            initializeMerge();
        }




        ///QuickSORT
        ///
        public int partition(int low, int high)
        {
            int pivot = values[high];
            List<int> temp;

            temp = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                temp.Add(values[x]);
                if (x == high)
                    temp.Add(2); // GOLD
                else if (x < low)
                    temp.Add(1); // GREEN
                else
                    temp.Add(0);
            }
            list.Add(temp);
            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                temp = new List<int>();
                for (int x = 0; x < 12; x++)
                {
                    temp.Add(values[x]);
                    if (x == high)
                        temp.Add(2);
                    else if (x <= i)
                        temp.Add(1);
                    else if (x == j)
                        temp.Add(3);
                    else if (x < j)
                        temp.Add(5);
                    else
                        temp.Add(0);
                }
                list.Add(temp);
                // If current element is smaller  
                // than the pivot 
                if (values[j] < pivot)
                {
                    if (j == i+1) 
                    {
                        i++;
                        continue;
                    }
                       
                    temp = new List<int>();
                    for (int x = 0; x < 12; x++)
                    {
                        
                        temp.Add(values[x]);
                        if (x == high)
                            temp.Add(2);
                        else if (x <= i)
                            temp.Add(1);
                        else if (x == j)
                            temp.Add(3); // RED
                        else if (x < j)
                            temp.Add(5);
                        else
                            temp.Add(0);
                    }
                    list.Add(temp);
                    i++;

                    // swap arr[i] and arr[j] 
                    int temps = values[i];
                    values[i] = values[j];
                    values[j] = temps;
                    temp = new List<int>();
                    for (int x = 0; x < 12; x++)
                    {
                        temp.Add(values[x]);
                        if (x == high)
                            temp.Add(2); // Gold
                        else if (x < i)
                            temp.Add(1); //Green
                        else if (x == j)
                            temp.Add(3); // RED
                        else if (x == i || x < j)
                            temp.Add(5); //Violet
                        else
                            temp.Add(0);
                    }
                    list.Add(temp);
                }
            }
            temp = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                temp.Add(values[x]);
                if (x == high)
                    temp.Add(2); // GOLD
                else if (x <= i)
                    temp.Add(1); //Green
                else if (x == i + 1)
                    temp.Add(3); //RED
                else if (x < high)
                    temp.Add(5); //Violet
                else
                    temp.Add(0);
            }
            list.Add(temp);
            // swap arr[i+1] and arr[high] (or pivot) 
            int temp1 = values[i + 1];
            values[i + 1] = values[high];
            values[high] = temp1;
            temp = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                temp.Add(values[x]);
                if (x == i + 1)
                    temp.Add(2); //GOLD
                else if (x <= i)
                    temp.Add(1); // Green
                else if (x == high)
                    temp.Add(3); //RED
                else if (x < high)
                    temp.Add(5); //Violet
                else
                    temp.Add(0);
            }
            list.Add(temp);
           
            return i + 1;
        }
        public void quickSort(int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(low, high);

                // Recursively sort elements before 
                // partition and after partition 
                quickSort(low, pi - 1);
                quickSort(pi + 1, high);
            }
        }
        public void initializeQuickSort()
        {
            timer1.Enabled = false;
            init = shuffle(5, 50);
            //MessageBox.Show(init.Count.ToString());
            values = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                values.Add(init[x]);
            }
            list.Clear();
            quickSort(0, 11);
            List<int> temp = new List<int>();
            for (int x = 0; x < 12; x++)
            {
                temp.Add(values[x]);
                temp.Add(2); //GOLD
            }
            list.Add(temp);
            frame = 0;
            timer1.Interval = trackBar1.Value;
            type = 'b';
            //timer1.Enabled = true;
            showList(list[0]);
            initializeTracker();
        }
        private void quickSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isMerge = false;
            button3.ImageIndex = 2;
            welcomePanel.Visible = false;
            title.Text = "Quick Sort";
            initializeQuickSort();
        }

    }




}
