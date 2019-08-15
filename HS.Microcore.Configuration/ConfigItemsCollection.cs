﻿#region Copyright 
// Copyright 2017 Gygya Inc.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.  
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED.  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;

namespace HS.Microcore.Configuration
{
    public class ConfigItemsCollection
    {
        private readonly Dictionary<string, ConfigItem> _data = new Dictionary<string, ConfigItem>(StringComparer.OrdinalIgnoreCase);

        public virtual IEnumerable<ConfigItem> Items => _data.Values;

        public ConfigItemsCollection(IEnumerable<ConfigItem> configItems)
        {
            foreach (var configItem in configItems)
            {
                var existing = TryGetConfigItem(configItem.Key);
                if (existing==null || existing.Priority < configItem.Priority)
                    _data[configItem.Key] = configItem;
            }
        }
        
        public virtual ConfigItem TryGetConfigItem(string key)
        {
            _data.TryGetValue(key, out ConfigItem configItem);
            return configItem;
        }

    }
}