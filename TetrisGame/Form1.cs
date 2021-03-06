﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Form1 : Form
    {
        private Color[] _blockEmptyColor;
        private Timer _timer;
        private int _tetrisPiecePosition;
        private bool[] _blocksFilled;

        public Form1()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Enabled = true;
            _timer.Tick += new System.EventHandler(TimerTickEvent);

            _tetrisPiecePosition = -3;
            _blockEmptyColor = new Color[2];
            _blockEmptyColor[0] = Color.LimeGreen;
            _blockEmptyColor[1] = Color.DarkSlateBlue;
            _blocksFilled = new bool[40];

            foreach (Label label in BlockLabels)
            {
                label.BackColor = _blockEmptyColor[0];
            }

            _timer.Start();
        }

        private void TimerTickEvent(object sender, EventArgs e)
        {
            _tetrisPiecePosition += 5;

            if(_tetrisPiecePosition < 35)
            {
                if(BlockLabels[_tetrisPiecePosition].BackColor == _blockEmptyColor[1])
                {
                    _tetrisPiecePosition = 2;
                }
            }

            if(_tetrisPiecePosition == 2)
            {
                int amount = 0;
                foreach (Label block in BlockLabels)
                {
                    if(block.BackColor == _blockEmptyColor[1])
                    {
                        _blocksFilled[amount] = true;
                    }
                    amount++;
                }
            }

            if(_tetrisPiecePosition > 40)
            {
                _tetrisPiecePosition = 2;
            }

            if(_tetrisPiecePosition < 40)
            {
                if (BlockLabels[_tetrisPiecePosition].BackColor == _blockEmptyColor[0])
                {
                    BlockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                    if(_tetrisPiecePosition > 5)
                    {
                        BlockLabels[_tetrisPiecePosition - 5].BackColor = _blockEmptyColor[0];
                    }
                }
            }

            if(BlockLabels[35].BackColor == _blockEmptyColor[1] &&
               BlockLabels[36].BackColor == _blockEmptyColor[1] &&
               BlockLabels[37].BackColor == _blockEmptyColor[1] &&
               BlockLabels[38].BackColor == _blockEmptyColor[1] &&
               BlockLabels[39].BackColor == _blockEmptyColor[1])
            {
                // Clear the filled bottom row
                BlockLabels[35].BackColor = _blockEmptyColor[0];
                BlockLabels[36].BackColor = _blockEmptyColor[0];
                BlockLabels[37].BackColor = _blockEmptyColor[0];
                BlockLabels[38].BackColor = _blockEmptyColor[0];
                BlockLabels[39].BackColor = _blockEmptyColor[0];

                int amountBlocks = 0;

                // make all _blocksFilled false
                _blocksFilled = new bool[40];

                // sets blue blocks to true
                foreach (Label block in BlockLabels)
                {
                    if(block.BackColor == _blockEmptyColor[1])
                    {
                        _blocksFilled[amountBlocks + 5] = true;
                    }
                    amountBlocks++;
                }

                // make all the piece empty
                foreach(Label block in BlockLabels)
                {
                    block.BackColor = _blockEmptyColor[0];
                }

                // make all pieces move down one row after completing row
                int amountBlue = 0;
                foreach (bool blockFilled in _blocksFilled)
                {
                    if(blockFilled == true)
                    {
                        BlockLabels[amountBlue].BackColor = _blockEmptyColor[1];
                    }
                    amountBlue++;
                }

                if(_tetrisPiecePosition < 40)
                {
                    if(blockLabels[_tetrisPiecePosition].BackColor == _blockEmptyColor[0])
                    {
                        BlockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                        if (_tetrisPiecePosition > 5)
                        {
                            BlockLabels[_tetrisPiecePosition - 5].BackColor = _blockEmptyColor[0];
                        }
                    }
                }
            }

            // when rows reach top
            if(BlockLabels[2].BackColor == _blockEmptyColor[1] && BlockLabels[2 + 5].BackColor == _blockEmptyColor[1])
            {
                _timer.Stop();
                DialogResult quitContinue = MessageBox.Show("You lose. Do you want to try again?", "Fail", MessageBoxButtons.YesNo);

                if(quitContinue == DialogResult.Yes)
                {
                    foreach (Label block in BlockLabels)
                    {
                        block.BackColor = _blockEmptyColor[0];
                    }
                    _blocksFilled = new bool[40];
                    _tetrisPiecePosition = -3;
                    _timer.Start();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Left)
            {
                if(_tetrisPiecePosition < 39 && _blocksFilled[_tetrisPiecePosition] == false)
                {
                    if(_tetrisPiecePosition != 0 && _tetrisPiecePosition != 5 && _tetrisPiecePosition != 10
                        && _tetrisPiecePosition != 15 && _tetrisPiecePosition != 20 && _tetrisPiecePosition != 25
                        && _tetrisPiecePosition != 30 && _tetrisPiecePosition != 35 && BlockLabels[_tetrisPiecePosition - 1].BackColor != _blockEmptyColor[1])
                    {
                        _tetrisPiecePosition--;
                        blockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                        BlockLabels[_tetrisPiecePosition + 1].BackColor = _blockEmptyColor[0];
                        return true;
                    }
                }
            }
            if (keyData == Keys.Right)
            {
                if (_tetrisPiecePosition < 39 && _blocksFilled[_tetrisPiecePosition] == false)
                {
                    if (_tetrisPiecePosition != 0 && _tetrisPiecePosition != 4 && _tetrisPiecePosition != 9
                        && _tetrisPiecePosition != 14 && _tetrisPiecePosition != 19 && _tetrisPiecePosition != 24
                        && _tetrisPiecePosition != 29 && _tetrisPiecePosition != 34 && BlockLabels[_tetrisPiecePosition + 1].BackColor != _blockEmptyColor[1])
                    {
                        _tetrisPiecePosition++;
                        blockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                        BlockLabels[_tetrisPiecePosition - 1].BackColor = _blockEmptyColor[0];
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
