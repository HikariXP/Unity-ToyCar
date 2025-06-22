/*
 * Author: CharSui
 * Created On: 2024.12.15
 * Description: 指令系统基类：
 * 指令系统：将其他模块的调用作为一项项的指令保存，此系统提到的帧数不一定是Unity的帧数，而是一次次的执行调用。
 * Update系列可能会有一个隐藏的问题：如果当前帧率过低，是否会造成指令消耗持续比指令输入慢一拍，导致操作延误？
 */

using System;
using Util;
using ValueExtension;

namespace InstructionSystem
{
    public abstract class BaseInstructionQueueSystem<T>
    {
        /// <summary>
        /// 指令数据类型
        /// </summary>
        protected Type instructionType => typeof(T);
    
        /// <summary>
        /// 待处理的指令
        /// </summary>
        private FixedQueue<T> instructions;
    
        /// <summary>
        /// 每帧处理指令数
        /// TODO:改为特性使用
        /// </summary>
        private uint perFrameProcessInstruction;
    
        /// <summary>
        /// 每多少帧执行一次
        /// </summary>
        private uint perProcessPassFrame;
    
        /// <summary>
        /// 执行前已经过了的帧数
        /// </summary>
        private uint _framePassBeforeProcess;
    
        /// <summary>
        /// 当队列已满，Ture则顶掉最旧的指令，False则不写入。
        /// </summary>
        private bool overwriteQueueWhenFull;
    
        /// <summary>
        /// 基础指令队列系统
        /// </summary>
        /// <param name="maxInstructionCached">最多缓存多少条指令</param>
        /// <param name="perFrameProcessInstruction">每多少帧执行一次</param>
        /// <param name="perProcessPassFrame">每帧最多处理多少个指令</param>
        /// <param name="overwriteQueueWhenFull">满指令的时候是否顶掉最旧的指令</param>
        protected BaseInstructionQueueSystem(int maxInstructionCached, uint perFrameProcessInstruction, uint perProcessPassFrame, bool overwriteQueueWhenFull)
        {
            // 获取最接近的二次方数
            var theClosePowerOfTwo = maxInstructionCached.NextPowerOfTwo();
            instructions = new FixedQueue<T>(theClosePowerOfTwo);

            // 进行初始化配置
            this.perFrameProcessInstruction = perFrameProcessInstruction;
            this.perProcessPassFrame = perProcessPassFrame;
            this.overwriteQueueWhenFull = overwriteQueueWhenFull;
        }
    
        /// <summary>
        /// 外部存放于Update或者什么其他逻辑里面的执行入口。
        /// 每一次调用FrameUpdate，会按照当前指令系统执行一次。
        /// </summary>
        public void FrameUpdate()
        {
            if(instructions.count == 0)return;
            
            // 如果当前有最大执行需要帧数，则跳过，比如每三帧执行一次。
            _framePassBeforeProcess += 1;
            if (_framePassBeforeProcess <= perProcessPassFrame) return;
            
            _framePassBeforeProcess = 1;

            // 当前方法执行会处理的指令数 : 如果是0，则当成处理所有的指令
            var processInstructionCount = perFrameProcessInstruction == 0 ? (uint)instructions.count : perFrameProcessInstruction;

            // 按照执行指令数进行指令的消费
            for (uint i = 0; i < processInstructionCount; i++)
            {
                // 获取指令
                var instruction = instructions.Dequeue();
                // 校验指令->指令已被取出，所以如果指令无效直接跳过即可。
                if(!InstructionVerify(instruction)) continue;
                // 执行指令
                FrameUpdateImplementation(instruction);
            }
        }
    
        /// <summary>
        /// 插入指令
        /// </summary>
        public void EnqueueInstruction(T instruction)
        {
            if (instructions.isFull)
            {
                if (overwriteQueueWhenFull)
                {
                    // 如果覆写，那么就先丢弃最旧的
                    instructions.Dequeue();
                }
                else
                {
                    // 不覆写，所以丢弃当前指令
                    return;
                }
            }
            
            instructions.Enqueue(instruction);
        }

        /// <summary>
        /// 清除当前缓存的所有指令
        /// </summary>
        public void ClearInstructions()
        {
            instructions.Clear();
        }

        /// <summary>
        /// 指令执行前进行校验
        /// </summary>
        /// <returns></returns>
        protected abstract bool InstructionVerify(T instruction);
    
        /// <summary>
        /// 实际执行调用
        /// </summary>
        protected abstract void FrameUpdateImplementation(T instruction);
    }
}

