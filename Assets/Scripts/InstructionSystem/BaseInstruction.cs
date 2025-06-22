/*
 * Author: CharSui
 * Created On: 2024.12.15
 * Description: 基础结构体，用于给指令系统使用
 */

namespace InstructionSystem
{
    public interface BaseInstruction
    {
        public ulong Uid { get; }
    }
}